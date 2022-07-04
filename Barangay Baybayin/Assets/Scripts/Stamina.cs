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

    private float currentStamina;
    private float currentMaxStamina;
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
    }
    private void OnEnable()
    {
        OnStaminaModifiedEvent.AddListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.AddListener(PenalizeStamina);
        TimeManager.onDayChangingEvent.AddListener(RegenerateStamina);
        currentMaxStamina = maxStamina; // some delay around here, when one starts the game and does Use(), THE PLAYER COULD FAINT
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
        
    }

    private void OnDisable()
    {
        OnStaminaModifiedEvent.RemoveListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.RemoveListener(PenalizeStamina);
        
        TimeManager.onDayChangingEvent.RemoveListener(RegenerateStamina);
        
        
    }
    void ManualSetStaminaEvent(float p_currentStamina)
    {
        currentStamina = p_currentStamina;
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
                OnStaminaModifiedEvent.Invoke(currentStamina, maxStamina);
            }
  
        }
        
    }
}
