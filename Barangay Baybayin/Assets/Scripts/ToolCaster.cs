using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToolUsed : UnityEvent<float> { }

public class ToolCanUseUpdated : UnityEvent<bool> { }
public class ToolCanSwitchUpdated : UnityEvent<bool> { }
public class ToolHitSucceeded : UnityEvent { }

public class SpecialUsed : UnityEvent { }

public class ToolCaster : MonoBehaviour
{
    [HideInInspector] public Tool current_Tool;
    private bool canUse = true;
    private bool canSwitch = true;
    [SerializeField] private float switchRate;
    private Transform aim;

    public ToolUsed onToolUsed = new ToolUsed();
    public ToolHitSucceeded onToolHitSucceeded = new ToolHitSucceeded();
    public ToolCanUseUpdated onToolCanUseUpdated = new ToolCanUseUpdated();
    public ToolCanSwitchUpdated onToolCanSwitchUpdated = new ToolCanSwitchUpdated();
    public SpecialUsed onSpecialUsed = new SpecialUsed();

    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
    }
    public void OnEnable()
    {
        if (GetComponent<Stamina>())
        {
            onToolUsed.AddListener(GetComponent<Stamina>().ModifyStamina);
        }

        onToolHitSucceeded.AddListener(ToolHitSuccess);

        ToolManager.onToolChanged.AddListener(OnToolChanged);
        onSpecialUsed.AddListener(OnSpecialUsed);
        OnToolChanged(ToolManager.instance.tools[0]);

    }

    public void OnDisable()
    {
        if (GetComponent<Stamina>())
        {
            onToolUsed.RemoveListener(GetComponent<Stamina>().ModifyStamina);
        }
        onToolHitSucceeded.RemoveListener(ToolHitSuccess);
        ToolManager.onToolChanged.RemoveListener(OnToolChanged);
        onSpecialUsed.RemoveListener(OnSpecialUsed);
    }


    #region SPECIAL

    public void OnSpecialUsed()
    {
        
        current_Tool.ModifySpecialAmount(-current_Tool.so_Tool.maxSpecialPoints[current_Tool.craftLevel]);
    }
  
    public void UseSpecial()
    {
        if (canUse)
        {
            if (current_Tool.specialPoints >= current_Tool.so_Tool.maxSpecialPoints[current_Tool.craftLevel])
            {

                ResourceNode targetResourceNode = GetResourceNode();
                if (targetResourceNode)
                {
                    Debug.Log("SPECIAL USED");
                    targetResourceNode.OnHit.Invoke(current_Tool.so_Tool.useForResourceNode,
                        current_Tool.craftLevel,
                        current_Tool.so_Tool.damage[current_Tool.craftLevel] * 2,
                        onSpecialUsed);
                    

                }
                StartCoroutine(Co_ToolUseCooldown());
            }
           
        }
        
    }
   
    #endregion

    public void OnToolChanged(Tool p_newTool)
    {
        current_Tool = p_newTool;
        StartCoroutine(Co_ToolSwitchCooldown());

    }

    public void UseTool()
    {
        if (canUse)
        {
            ResourceNode targetResourceNode = GetResourceNode();
            if (targetResourceNode)
            {
                targetResourceNode.OnHit.Invoke(current_Tool.so_Tool.useForResourceNode,
                    current_Tool.craftLevel,
                    current_Tool.so_Tool.damage[current_Tool.craftLevel],
                    onToolHitSucceeded);
                

            }
            StartCoroutine(Co_ToolUseCooldown());
        }

    }
    ResourceNode GetResourceNode() 
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, 3f);
        foreach (Collider2D hit in collider)
        {
            if (hit.gameObject != gameObject)
            {
                if (hit != null)
                {
                    
                    return hit.gameObject.GetComponent<ResourceNode>(); 
                    
                }
            }

        }
        return null;
    }

    public void ToolHitSuccess()
    {

        current_Tool.ModifyProficiencyAmount(current_Tool.so_Tool.proficiencyAmountReward[current_Tool.craftLevel]);
        current_Tool.ModifySpecialAmount(current_Tool.so_Tool.specialPointReward[current_Tool.craftLevel]); 
    }
   
    IEnumerator Co_ToolUseCooldown()
    {
        canUse = false;
        onToolCanUseUpdated.Invoke(canUse);
        onToolUsed.Invoke(current_Tool.so_Tool.staminaCost[current_Tool.craftLevel]);
        yield return new WaitForSeconds(current_Tool.so_Tool.useRate[current_Tool.craftLevel]);
        canUse = true;
        onToolCanUseUpdated.Invoke(canUse);
    }

    IEnumerator Co_ToolSwitchCooldown()
    {
        canSwitch = false;
        onToolCanSwitchUpdated.Invoke(canSwitch);

        yield return new WaitForSeconds(switchRate);
        canSwitch = true;
        onToolCanSwitchUpdated.Invoke(canSwitch);
    }



}
