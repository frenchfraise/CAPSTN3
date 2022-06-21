using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToolUsedEvent : UnityEvent<float> { }

public class ToolCanUseUpdatedEvent : UnityEvent<bool> { }
public class ToolCanSwitchUpdatedEvent : UnityEvent<bool> { }
public class ToolHitSucceededEvent : UnityEvent { }

public class ToolSpecialUseEvent : UnityEvent { }

public class LongClickEvent : UnityEvent { }
public class ToolCaster : MonoBehaviour
{
 
    private bool isPointerDown;
    private float pointerDownTimer; // this can be changed
    // public float requiredHoldTime;
    private int chargeCounter;
    private float staminaCost;

    public LongClickEvent onLongClickEvent;

    [HideInInspector] public Tool current_Tool;
    private bool isTax = false;
    private bool canUse = true;
    private bool canSwitch = true;
    [SerializeField] private float switchRate;
    private Transform aim;

    public Animator animator;

    public ToolUsedEvent onToolUsedEvent = new ToolUsedEvent();
    public ToolHitSucceededEvent onToolHitSucceededEvent = new ToolHitSucceededEvent();
    public ToolCanUseUpdatedEvent onToolCanUseUpdatedEvent = new ToolCanUseUpdatedEvent();
    public ToolCanSwitchUpdatedEvent onToolCanSwitchUpdatedEvent = new ToolCanSwitchUpdatedEvent();
    public ToolSpecialUseEvent onToolSpecialUsedEvent = new ToolSpecialUseEvent();

    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
    }
    public void OnEnable()
    {
        WeatherManager.onWeatherChangedEvent.AddListener(CheckWeatherStaminaTax);

        if (GetComponent<Stamina>())
        {
            onToolUsedEvent.AddListener(GetComponent<Stamina>().ModifyStamina);
        }

        onToolHitSucceededEvent.AddListener(ToolHitSuccess);

        ToolManager.onToolChangedEvent.AddListener(OnToolChanged);
        onToolSpecialUsedEvent.AddListener(OnSpecialUsed);
        OnToolChanged(ToolManager.instance.tools[0]); // temporary (?)
        canUse = true;
        canSwitch = true;
    }

    public void OnDisable()
    {
        WeatherManager.onWeatherChangedEvent.RemoveListener(CheckWeatherStaminaTax);

        if (GetComponent<Stamina>())
        {
            onToolUsedEvent.RemoveListener(GetComponent<Stamina>().ModifyStamina);
        }
        onToolHitSucceededEvent.RemoveListener(ToolHitSuccess);
        ToolManager.onToolChangedEvent.RemoveListener(OnToolChanged);
        onToolSpecialUsedEvent.RemoveListener(OnSpecialUsed);
    }

    public void OnSpecialUsed()
    {
        // I'm guessing this is where it decrements when it is "full"
        // current_Tool.ModifySpecialAmount(-current_Tool.so_Tool.maxSpecialPoints[current_Tool.craftLevel]);
        current_Tool.specialChargesCounter--;
    }
  
    public void UseSpecial()
    {
        if (canUse)
        {
            if (current_Tool.specialChargesCounter >= 1)
            {
                ResourceNode targetResourceNode = GetResourceNode();
                if (targetResourceNode)
                {
                    float xPos = targetResourceNode.transform.position.x;
                    Debug.Log("SPECIAL USED");
                    targetResourceNode.OnResourceNodeHitEvent.Invoke(current_Tool.so_Tool.useForResourceNode,
                        current_Tool.craftLevel,
                        current_Tool.so_Tool.damage[current_Tool.craftLevel] * 2,
                        onToolSpecialUsedEvent);
                }
                StartCoroutine(Co_ToolUseCooldown());
            }
           
        }
        
    }
    public void OnToolChanged(Tool p_newTool)
    {
        current_Tool = p_newTool;
        StartCoroutine(Co_ToolSwitchCooldown());
    }
    private void Update()
    {     
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;            
            if (pointerDownTimer >= current_Tool.so_Tool.chargeSpeedRate)
            {
                // Debug.Log("pointerDownTimer reached.");
                onLongClickEvent?.Invoke();
               
                if (chargeCounter != current_Tool.so_Tool.maxToolCharge)
                {
                    chargeCounter++;
                    Debug.Log("Charge: " + chargeCounter + "/" + current_Tool.so_Tool.maxToolCharge);
                }
                Reset();
            }
        }
    }   

    IEnumerator Co_Cooldown()
    {
        yield return new WaitForSeconds(current_Tool.so_Tool.useRate[current_Tool.craftLevel]);
        canUse = true;
    }
    public void UseTool()
    {
        if (canUse)
        {
            // animator.SetTrigger("UseTool");            
            animator.SetTrigger(current_Tool.toolName.ToString());

            ResourceNode targetResourceNode = GetResourceNode();
            if (targetResourceNode)
            {
                float xPos = targetResourceNode.transform.position.x;
                if (xPos > PlayerManager.instance.player.transform.position.x) // right
                {
                    animator.SetBool("isFacingRight", true);
                }
                else // left
                {
                    animator.SetBool("isFacingRight", false);
                }
                targetResourceNode.OnResourceNodeHitEvent.Invoke(current_Tool.so_Tool.useForResourceNode,
                   current_Tool.craftLevel,
                   current_Tool.so_Tool.damage[current_Tool.craftLevel],
                   onToolHitSucceededEvent);
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
        // animator.SetTrigger("UseTool");
        onToolCanUseUpdatedEvent.Invoke(canUse);
        onToolUsedEvent.Invoke(staminaCost);
        yield return new WaitForSeconds(current_Tool.so_Tool.useRate[current_Tool.craftLevel]);
        canUse = true;
        onToolCanUseUpdatedEvent.Invoke(canUse);
    }
    IEnumerator Co_ToolSwitchCooldown()
    {
        canSwitch = false;
        onToolCanSwitchUpdatedEvent.Invoke(canSwitch);        
        yield return new WaitForSeconds(switchRate);
        canSwitch = true;
        onToolCanSwitchUpdatedEvent.Invoke(canSwitch);
    }

    private void CheckWeatherStaminaTax(Weather p_currentWeather, Weather p_nextWeather)
    {
        if (Weather.Rainy == p_currentWeather)
        {
            staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel] * 1.5f;
        }
        else staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel];
    }

    public void OnPointerDown()
    {
        isPointerDown = true;
        Debug.Log("[OnPointerDown] Charging...");
    }

    public void OnPointerUp()
    {
        Reset();
        Debug.Log("[OnPointerUp] Charging done.");
    }

    private void Reset()
    {
        isPointerDown = false;
        pointerDownTimer = 0;
        // Invoke Tool Charge Use()
    }    
}