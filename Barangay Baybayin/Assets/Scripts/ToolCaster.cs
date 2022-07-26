using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SetRequireCorrectToolEvent : UnityEvent<Tool> { }
public class SetRequireCorrectToolSwingEvent : UnityEvent<bool> { }
public class SetIsPreciseEvent : UnityEvent<bool> { }
public class ToolUsedEvent : UnityEvent<float> { }

public class ToolCanUseUpdatedEvent : UnityEvent<bool> { }
public class ToolCanSwitchUpdatedEvent : UnityEvent<bool> { }
public class ToolHitSucceededEvent : UnityEvent { }

public class ToolSpecialUseEvent : UnityEvent { }

public class LongClickEvent : UnityEvent { }
public class ToolCaster : MonoBehaviour
{
    public float critMultiplier;
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
    private bool isPrecise = false;
    private Tool requiredTool = null;
    [SerializeField] private float switchRate;
    private Transform aim;

    public Animator animator;
    [SerializeField] private float detectionRadius;
    public static ToolUsedEvent onToolUsedEvent = new ToolUsedEvent();
    public ToolHitSucceededEvent onToolHitSucceededEvent = new ToolHitSucceededEvent();
    public ToolCanUseUpdatedEvent onToolCanUseUpdatedEvent = new ToolCanUseUpdatedEvent();
    public ToolCanSwitchUpdatedEvent onToolCanSwitchUpdatedEvent = new ToolCanSwitchUpdatedEvent();
    public static ToolSpecialUseEvent onToolSpecialUsedEvent = new ToolSpecialUseEvent();
    public static SetIsPreciseEvent onSetIsPreciseEvent = new SetIsPreciseEvent();
    public static SetRequireCorrectToolEvent onSetRequireCorrectToolEvent = new SetRequireCorrectToolEvent();
    public static SetRequireCorrectToolSwingEvent onSetRequireCorrectToolSwingEvent = new SetRequireCorrectToolSwingEvent();
    
    private bool isFirstTime;
    private bool rewardSpecialAllowed = true;
    bool requireCorrectTool = false;
    private int currentCharges;
    private void Awake()
    {
        aim = GetComponent<PlayerJoystick>().aim;
        ToolManager.onToolChangedEvent.AddListener(OnToolChanged);
        //onCriticalFirstTimeEvent.AddListener(FirstTime);

        ToolManager.onSpecialPointsFilledEvent.AddListener(FirstTime);
        onSetRequireCorrectToolEvent.AddListener(SetRequireCorrectTool);
        onSetRequireCorrectToolSwingEvent.AddListener(SetRequireCorrectToolEvent);
        onSetIsPreciseEvent.AddListener(SetIsPrecise);
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
        onSetRequireCorrectToolEvent.RemoveListener(SetRequireCorrectTool);
        onSetRequireCorrectToolSwingEvent.RemoveListener(SetRequireCorrectToolEvent);
        onSetIsPreciseEvent.RemoveListener(SetIsPrecise);
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
            isFirstTime = false;
            ToolManager.onSpecialPointsFilledEvent.RemoveListener(FirstTime);
            TutorialManager.instance.tutorialUI.RemindTutorialEvent(3);
        }
 

    }
    public void OnEnable()
    {
     


        //onToolSpecialUsedEvent.AddListener(OnSpecialUsed);

        canUse = true;
        canSwitch = true;
    }

    public void OnDisable()
    {
        
        //onToolSpecialUsedEvent.RemoveListener(OnSpecialUsed);
    }
    void SetRequireCorrectTool(Tool p_isPrecise)
    {
        requiredTool = p_isPrecise;
    }
    void SetRequireCorrectToolEvent(bool p_RequireCorrectTool)
    {
        requireCorrectTool = p_RequireCorrectTool;
    }
    void SetIsPrecise(bool p_isPrecise)
    {
        isPrecise = p_isPrecise;
    }
    public void OnSpecialUsed()
    {
        // I'm guessing this is where it decrements when it is "full"
        // current_Tool.ModifySpecialAmount(-current_Tool.so_Tool.maxSpecialPoints[current_Tool.craftLevel]);
        
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
                           current_Tool.so_Tool.damage[current_Tool.craftLevel],
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
                        //Debug.Log("SPECIAL USED");
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
        if (p_bool) onToolUsedEvent.Invoke(staminaCost);
        //Debug.Log("Tool Cooldown Use Rate: " + current_Tool.so_Tool.useRate[current_Tool.craftLevel]);
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

    public void OnPointerDown()
    {
        isPointerDown = true;
        //Debug.Log("[OnPointerDown] Charging...");
    }

    public void OnPointerUp()
    {
        Reset();
       // Debug.Log("[OnPointerUp] Charging done.");
    }

    private void Reset()
    {
        isPointerDown = false;
        pointerDownTimer = 0;
        // Invoke Tool Charge Use()
    }    
}