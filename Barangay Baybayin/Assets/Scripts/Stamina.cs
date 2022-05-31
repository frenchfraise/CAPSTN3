using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaminaDecrease : UnityEvent<float,float> { }
public class StaminaDepleted : UnityEvent{ }
public class Stamina : MonoBehaviour
{
    [SerializeField] private GenericBarUI genericBarUI;

    private float currentStamina;
    private float currentMaxStamina;
    [SerializeField] private float maxStamina;

    [SerializeField] private float staminaFatiguePenalty;
    [SerializeField] private float staminaFatigueRecovery; // not implemented

    private bool isPenalized = false;

    public StaminaDecrease OnStaminaModified = new StaminaDecrease();
    public StaminaDepleted onStaminaDepleted = new StaminaDepleted();

   
   
    private void OnEnable()
    {
        OnStaminaModified.AddListener(genericBarUI.UpdateBar);
        onStaminaDepleted.AddListener(PenalizeStamina);
        TimeManager.onDayChanged.AddListener(RegenerateStamina);
        currentMaxStamina = maxStamina;
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
    }

    private void OnDisable()
    {
        OnStaminaModified.RemoveListener(genericBarUI.UpdateBar);
        onStaminaDepleted.RemoveListener(PenalizeStamina);
        if (TimeManager.instance)
        {
            TimeManager.onDayChanged.RemoveListener(RegenerateStamina);
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
            onStaminaDepleted.Invoke();
         
        }
        else
        {
            OnStaminaModified.Invoke(currentStamina, maxStamina);
        }
        
    }
}
