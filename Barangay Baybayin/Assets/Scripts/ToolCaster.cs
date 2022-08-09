using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.VFX;

public class ToolUsedEvent : UnityEvent<float> { }

public class ToolCanUseUpdatedEvent : UnityEvent<bool> { }
public class ToolCanSwitchUpdatedEvent : UnityEvent<bool> { }
public class ToolHitSucceededEvent : UnityEvent { }

public class ToolSpecialUseEvent : UnityEvent { }

public class LongClickEvent : UnityEvent { }
public class ToolCaster : MonoBehaviour
{
    public float critMultiplier;
    private float staminaCost;
    private float useRate;

    [SerializeField] private Image toolImageCooldown;

    [HideInInspector] public Tool current_Tool;
    private bool canUse = true;
    private bool canSwitch = true;
    private bool isPrecise = false;
    private Tool requiredTool = null;
    [SerializeField] private float switchRate;
    private Transform aim;

    public VisualEffect onHitVFX;   

    public Animator animator;
    [SerializeField] private float detectionRadius;
    public static ToolUsedEvent onToolUsedEvent = new ToolUsedEvent();
    public ToolHitSucceededEvent onToolHitSucceededEvent = new ToolHitSucceededEvent();
    public ToolCanUseUpdatedEvent onToolCanUseUpdatedEvent = new ToolCanUseUpdatedEvent();
    public ToolCanSwitchUpdatedEvent onToolCanSwitchUpdatedEvent = new ToolCanSwitchUpdatedEvent();
    public static ToolSpecialUseEvent onToolSpecialUsedEvent = new ToolSpecialUseEvent();

    
    private bool rewardSpecialAllowed = true;
    bool requireCorrectTool = false;
    private int currentCharges;
    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
        ToolManager.onToolChangedEvent.AddListener(OnToolChanged);
        //onCriticalFirstTimeEvent.AddListener(FirstTime);

        ToolManager.onSpecialPointsFilledEvent.AddListener(FirstTime);

        WeatherManager.onWeatherChangedEvent.AddListener(CheckWeatherStaminaTax);

        if (TryGetComponent<Stamina>(out Stamina stamina))
        {
            onToolUsedEvent.AddListener(stamina.ModifyStamina);
        }

        onToolHitSucceededEvent.AddListener(ToolHitSuccess);
        //ToolManager.onToolChangedEvent.Invoke(ToolManager.instance.tools[0]);

    }
    private void OnDestroy()
    {
        ToolManager.onToolChangedEvent.RemoveListener(OnToolChanged);
        ToolManager.onSpecialPointsFilledEvent.RemoveListener(FirstTime);
   
        WeatherManager.onWeatherChangedEvent.RemoveListener(CheckWeatherStaminaTax);

        if (GetComponent<Stamina>())
        {
            onToolUsedEvent.RemoveListener(GetComponent<Stamina>().ModifyStamina);
        }
        onToolHitSucceededEvent.RemoveListener(ToolHitSuccess);
       
    }
    private void FirstTime()
    {
        if (!TimeManager.instance.tutorialOn)
        {
            ToolManager.onSpecialPointsFilledEvent.RemoveListener(FirstTime);
            TutorialManager.instance.tutorialUI.RemindTutorialEvent(3);
        }
 

    }
    public void OnEnable()
    {
        canUse = true;
        canSwitch = true;
    }

    public void SetRequireCorrectTool(Tool p_isPrecise)
    {
        requiredTool = p_isPrecise;
    }
    public void SetRequireCorrectToolEvent(bool p_RequireCorrectTool)
    {
        requireCorrectTool = p_RequireCorrectTool;
    }
    public void SetIsPrecise(bool p_isPrecise)
    {
        isPrecise = p_isPrecise;
    }
    
    public void UseSpecial()
    {
        if (canUse)
        {
            if (current_Tool.specialChargesCounter >= 1)
            {
                rewardSpecialAllowed = false;
        
                animator.SetTrigger(current_Tool.toolName.ToString());
                AudioManager.instance.GetSoundByName("Swing").source.Play();
                if (current_Tool.toolName == "Hammer")
                {
                    Infrastructure targetInfrastructure = GetInfrastructure();
                    if (targetInfrastructure)
                    {
                        float xPos = targetInfrastructure.transform.position.x;
                        if (xPos > transform.position.x) // right
                        {
                            animator.SetBool("isFacingRight", true);
                        }
                        else // left
                        {
                            animator.SetBool("isFacingRight", false);
                        }
                        current_Tool.specialChargesCounter--;
                        targetInfrastructure.OnInfrastructureHitEvent.Invoke(
                           current_Tool.craftLevel,
                           current_Tool.so_Tool.damage[current_Tool.craftLevel] * critMultiplier,
                           onToolSpecialUsedEvent);
                    }
                }
                else
                {
                    animator.SetTrigger(current_Tool.toolName.ToString());
                    AudioManager.instance.GetSoundByName("Swing").source.Play();
                    ResourceNode targetResourceNode = GetResourceNode();
                    if (targetResourceNode)
                    {
                        float xPos = targetResourceNode.transform.position.x;
                        if (xPos > transform.position.x) // right
                        {
                            animator.SetBool("isFacingRight", true);
                        }
                        else // left
                        {
                            animator.SetBool("isFacingRight", false);
                        }
                        current_Tool.specialChargesCounter--;
                        targetResourceNode.OnResourceNodeHitEvent.Invoke(current_Tool.so_Tool.useForResourceNode,
                            current_Tool.craftLevel,
                            current_Tool.so_Tool.damage[current_Tool.craftLevel] * critMultiplier,
                            onToolSpecialUsedEvent);
                    }
                }
                //Debug.Log("Use Special call Coroutine!");
                StartCoroutine(Co_ToolUseCooldown(false));
            }           
        }        
    }
    public void OnToolChanged(Tool p_newTool)
    {
        current_Tool = p_newTool;
        staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel];
        StartCoroutine(Co_ToolSwitchCooldown());
    }

    public void UseTool()
    {
        bool canHit = false;
        bool temprequireCorrectTool = false;
        if (canUse)
        {
            rewardSpecialAllowed = true;
            if (requiredTool == null ||
               
                requiredTool != null && current_Tool == requiredTool)
            {
                // animator.SetTrigger("UseTool");            
                animator.SetTrigger(current_Tool.toolName.ToString());
                AudioManager.instance.GetSoundByName("Swing").source.Play();
                

                if (current_Tool.toolName == "Hammer")
                {
                    Infrastructure targetInfrastructure = GetInfrastructure();
                    if (targetInfrastructure)
                    {

                        float xPos = targetInfrastructure.transform.position.x;
                        if (xPos > transform.position.x) // right
                        {
                            animator.SetBool("isFacingRight", true);
                        }
                        else // left
                        {
                            animator.SetBool("isFacingRight", false);
                        }
                        onHitVFX.transform.position = targetInfrastructure.transform.position;
                        onHitVFX.Play();
                        canHit = true;
                        temprequireCorrectTool = true;
                        targetInfrastructure.OnInfrastructureHitEvent.Invoke(
                           current_Tool.craftLevel,
                           current_Tool.so_Tool.damage[current_Tool.craftLevel],
                           onToolHitSucceededEvent);
                    }
                }
                else
                {
                    ResourceNode targetResourceNode = GetResourceNode();
                    if (targetResourceNode)
                    {
                        float xPos = targetResourceNode.transform.position.x;
                        if (xPos > transform.position.x) // right
                        {
                            animator.SetBool("isFacingRight", true);
                        }
                        else // left
                        {
                            animator.SetBool("isFacingRight", false);
                        }
                        canHit = true;
                        for (int i = 0; i < current_Tool.so_Tool.useForResourceNode.Count; i++)
                        {
                            if (targetResourceNode.so_ResourceNode == current_Tool.so_Tool.useForResourceNode[i])
                            {
                                temprequireCorrectTool = true;

                                break;
                            }

                        }
                        onHitVFX.transform.position = new Vector2(targetResourceNode.transform.position.x, targetResourceNode.transform.position.y + 5);
                        onHitVFX.Play();
                        targetResourceNode.OnResourceNodeHitEvent.Invoke(current_Tool.so_Tool.useForResourceNode,
                           current_Tool.craftLevel,
                           current_Tool.so_Tool.damage[current_Tool.craftLevel],
                           onToolHitSucceededEvent);
                    }
                }
                if (!isPrecise || isPrecise && canHit)
                {
                    //Debug.Log("PRESSED" + " " + isPrecise + " " + current_Tool.so_Tool.staminaCost[current_Tool.craftLevel] + " " + canHit + " " + current_Tool.so_Tool.useRate[current_Tool.craftLevel]);
                  
                    if (requireCorrectTool && !temprequireCorrectTool)
                    {
                        StorylineManager.onWorldEventEndedEvent.Invoke("SWINGINGWRONGTOOL", 0, 0);
                    }
                    else
                    {
                      
                        StartCoroutine(Co_ToolUseCooldown(true));
                    }
                }
                else if (isPrecise && !canHit)
                {
                    //if (requireCorrectTool && !temprequireCorrectTool)
                    //{
                    //    StorylineManager.onWorldEventEndedEvent.Invoke("SWINGINGWRONGTOOL", 0, 0);
                    //}
                    //else

                    //{
                        StorylineManager.onWorldEventEndedEvent.Invoke("SWINGINGINAIR", 0, 0);
                    //}
               
                }
            }
            else
            {
                StorylineManager.onWorldEventEndedEvent.Invoke("SWINGINGWRONGTOOL", 0, 0);
            }
        }
    }
    public ResourceNode GetResourceNode() 
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, detectionRadius);
        foreach (Collider2D hit in collider)
        {
            if (hit != null)
            {
              //  Debug.Log(hit.gameObject.name + " node");
                if (hit.gameObject != gameObject)
                {
                   // Debug.Log(hit + " phase 1 ");
                    if (hit.TryGetComponent<ResourceNode>(out ResourceNode resourceNode))
                    {
                        if (resourceNode.TryGetComponent<Health>(out Health health))
                        {
                            if (health.isAlive)
                            {
                                // Debug.Log("FOUND RESOURCE NODE");
                                return resourceNode;
                            }
                        }
                       
                    }                        
                    
                }
            }
        }
        return null;
    }

    public Infrastructure GetInfrastructure()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)aim.position, detectionRadius);
        foreach (Collider2D hit in collider)
        {
            if (hit != null)
            {
                if (hit.gameObject != gameObject)
                {

                    if (hit.TryGetComponent<Infrastructure>(out Infrastructure infrastructure))
                    {
                        if (infrastructure.TryGetComponent<Health>(out Health health))
                        {
                            if (health.isAlive)
                            {
                                // Debug.Log("FOUND RESOURCE NODE");
                                return infrastructure;
                            }
                        }
                      
                    }
                }
            }
        }
        return null;
    }

    public void ToolHitSuccess()
    {
        AudioManager.instance.GetSoundByName("Hit").source.Play();
        current_Tool.ModifyProficiencyAmount(current_Tool.so_Tool.proficiencyAmountReward[current_Tool.craftLevel]);
        if (rewardSpecialAllowed)
        {
            current_Tool.ModifySpecialAmount(current_Tool.so_Tool.specialPointReward[current_Tool.craftLevel]);
            rewardSpecialAllowed = true;
        }
        

    }

    IEnumerator Co_ToolUseCooldown(bool p_bool)
    {
        canUse = false;        
        onToolCanUseUpdatedEvent.Invoke(canUse);

        //Debug.Log(current_Tool.so_Tool.staminaCost[current_Tool.craftLevel]);
        if (p_bool)
        {
            onToolUsedEvent.Invoke(staminaCost);
        }

        useRate = current_Tool.so_Tool.useRate[current_Tool.craftLevel];

        while (!canUse)
        {
            useRate -= Time.deltaTime;
            yield return null;// new WaitForSeconds(Time.deltaTime);

            if (useRate <= 0)
            {
                canUse = true;
                onToolCanUseUpdatedEvent.Invoke(canUse);
                toolImageCooldown.fillAmount = 0;
                break;
            }
            else
            {
                toolImageCooldown.fillAmount = useRate / current_Tool.so_Tool.useRate[current_Tool.craftLevel];
            }
        }        
    }
    IEnumerator Co_ToolSwitchCooldown()
    {
        canSwitch = false;
        onToolCanSwitchUpdatedEvent.Invoke(canSwitch);        
        yield return new WaitForSeconds(switchRate);
        canSwitch = true;
        onToolCanSwitchUpdatedEvent.Invoke(canSwitch);
    }

    private void CheckWeatherStaminaTax(List<Weather> p_weathers, List<Weather> p_currentWeathers)
    {
        //Debug.Log(p_currentWeathers[0].name + " - " + current_Tool.so_Tool.staminaCost[current_Tool.craftLevel - 1]);
        if (p_weathers[2].name == p_currentWeathers[0].name)
        {
            staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel] * 1.5f;
            Debug.Log("It is rainy! Tax is: " + staminaCost);
        }
        else if (p_currentWeathers[0] == null)
        {
            Debug.Log("No tax applied!");
            staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel];
        }
        else
        {
            Debug.Log("No tax applied!");
            staminaCost = current_Tool.so_Tool.staminaCost[current_Tool.craftLevel];           
        }
    } 
}