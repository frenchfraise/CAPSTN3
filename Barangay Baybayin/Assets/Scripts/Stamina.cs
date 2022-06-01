using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public StaminaDepletedEvent onStaminaDepletedEvent = new StaminaDepletedEvent();

   
   
    private void OnEnable()
    {
        OnStaminaModifiedEvent.AddListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.AddListener(PenalizeStamina);
        TimeManager.onDayChangedEvent.AddListener(RegenerateStamina);
        currentMaxStamina = maxStamina;
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
    }

    private void OnDisable()
    {
        OnStaminaModifiedEvent.RemoveListener(genericBarUI.UpdateBar);
        onStaminaDepletedEvent.RemoveListener(PenalizeStamina);
        if (TimeManager.instance)
        {
            TimeManager.onDayChangedEvent.RemoveListener(RegenerateStamina);
        }
        
    }

    public void PenalizeStamina()
    {
        currentMaxStamina = currentMaxStamina - staminaFatiguePenalty;
        isPenalized = true;
        RegenerateStamina(1);
    }
    public void RegenerateStamina(int p_day)
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
            OnStaminaModifiedEvent.Invoke(currentStamina, maxStamina);
        }
        
    }
}
