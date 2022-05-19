using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StaminaDecrease : UnityEvent<float,float> { }
public class Stamina : MonoBehaviour
{
    [SerializeField] private GenericBarUI genericBarUI;
    public float currentStamina;
    public float maxStamina;

    public StaminaDecrease OnStaminaDecrease = new StaminaDecrease();

    private void OnEnable()
    {
        OnStaminaDecrease.AddListener(genericBarUI.UpdateBar);
    }

    private void OnDisable()
    {
        OnStaminaDecrease.AddListener(genericBarUI.UpdateBar);
    }

    public void StaminaDecreased(float p_amount)
    {
        if (currentStamina > 0)
        {
            currentStamina -= p_amount;
        }
  
        OnStaminaDecrease.Invoke(currentStamina,maxStamina);
    }
}
