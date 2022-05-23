using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class StaminaDecrease : UnityEvent<float,float> { }
[System.Serializable]
public class StaminaDepleted : UnityEvent{ }
public class Stamina : MonoBehaviour
{
    [SerializeField] private GenericBarUI genericBarUI;
    [SerializeField] private float currentStamina;
    [SerializeField] private float staminaPenalty;
    private float currentMaxStamina;
    [SerializeField] private float maxStamina;
    private bool isPenalized = false;

    public StaminaDecrease OnStaminaModified = new StaminaDecrease();
    public StaminaDepleted onStaminaDepleted = new StaminaDepleted();
    private void OnEnable()
    {
        OnStaminaModified.AddListener(genericBarUI.UpdateBar);
        onStaminaDepleted.AddListener(PenalizeStamina);
        TimeManager.instance.onDayChanged.AddListener(RegenerateStamina);
        currentMaxStamina = maxStamina;
        genericBarUI.InstantUpdateBar(currentStamina, currentMaxStamina, maxStamina);
    }

    private void OnDisable()
    {
        OnStaminaModified.RemoveListener(genericBarUI.UpdateBar);
        onStaminaDepleted.RemoveListener(PenalizeStamina);
        if (TimeManager.instance)
        {
            TimeManager.instance.onDayChanged.RemoveListener(RegenerateStamina);
        }
        
    }

    public void PenalizeStamina()
    {
        currentMaxStamina = currentMaxStamina - staminaPenalty;
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
                currentMaxStamina = currentMaxStamina + staminaPenalty;
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
