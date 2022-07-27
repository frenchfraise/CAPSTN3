using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManualSetStaminaEvent : UnityEvent<float> { }
public class StaminaDecreaseEvent : UnityEvent<float,float> { }
public class StaminaDepletedEvent : UnityEvent{ }
public class Stamina : MonoBehaviour
{
    [SerializeField] private GenericBarUI genericBarUI;

    [HideInInspector] public float currentStamina { get; private set; }
    [HideInInspector] public float currentMaxStamina { get; private set; }
    [SerializeField] private float maxStamina;

    [SerializeField] private float staminaFatiguePenalty;
    [SerializeField] private float staminaFatigueRecovery; // not implemented

    private bool isPenalized = false;

    public StaminaDecreaseEvent OnStaminaModifiedEvent = new StaminaDecreaseEvent();
    public static StaminaDepletedEvent onStaminaDepletedEvent = new StaminaDepletedEvent();
    public static ManualSetStaminaEvent onManualSetStaminaEvent = new ManualSetStaminaEvent();

    private void Awake()
    {
        onManualSetStaminaEvent.AddListener(ManualSetStaminaEvent);
        OnStaminaModifiedEvent.AddListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.AddListener(PenalizeStamina);
        TimeManager.onDayChangingEvent.AddListener(RegenerateStamina);
        currentMaxStamina = maxStamina; // some delay around here, when one starts the game and does Use(), THE PLAYER COULD FAINT
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
       // Debug.Log("STAM ENABLED");
    }

    private void Destroy()
    {
        OnStaminaModifiedEvent.RemoveListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.RemoveListener(PenalizeStamina);
        TimeManager.onDayChangingEvent.RemoveListener(RegenerateStamina);
    }
    private void OnEnable()
    {
        
        
  
        
    }

    private void OnDisable()
    {
 
 
        

        
        
    }
    void ManualSetStaminaEvent(float p_currentStamina)
    {
        currentStamina = p_currentStamina;
        //float amount = Mathf.Abs(p_currentStamina - currentStamina);
        //ModifyStamina(amount);
        genericBarUI.InstantUpdateBar(currentStamina, maxStamina, maxStamina);
      //  Debug.Log("MANUAL STAM ENABLED");
    }
    public void PenalizeStamina()
    {
        currentMaxStamina = currentMaxStamina - staminaFatiguePenalty;
        isPenalized = true;
        RegenerateStamina();
    }
    public void RegenerateStamina()
    {
        if (isPenalized)
        {
            isPenalized = false;
        }
        else
        {
            if (currentMaxStamina < maxStamina)
            {
                currentMaxStamina = currentMaxStamina + staminaFatiguePenalty;
            }
            if (currentMaxStamina > maxStamina)
            {
                currentMaxStamina = maxStamina;
            }

        }
        currentStamina = currentMaxStamina;
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
    }

    public void ModifyStamina(float p_amount)
    {
        Debug.Log("STAM STAM " + p_amount);
        if (currentStamina > 0)
        {
            currentStamina -= p_amount;
        }
        if (currentStamina <= 0)
        {
            onStaminaDepletedEvent.Invoke();
         
        }
        else
        {
            if (UIManager.instance.gameplayHUD.activeSelf == true)
            {
               // Debug.Log("OnStaminaModifiedEvent.Invoke!");
                OnStaminaModifiedEvent.Invoke(currentStamina, maxStamina);
            }
  
        }  
    }

    public void IncrementStamina(float p_amount)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += p_amount;
        }
        else if (currentStamina >= maxStamina)
        {
            Debug.Log("Stamina is full you don't have to eat!");
        }
        if (UIManager.instance.gameplayHUD.activeSelf)
        {
            OnStaminaModifiedEvent.Invoke(currentStamina, maxStamina);
        }
    }
}
