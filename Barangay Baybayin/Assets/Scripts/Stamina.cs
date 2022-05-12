using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StaminaDecrease : UnityEvent<Stamina> { }
public class Stamina : MonoBehaviour
{
    public static Stamina instance; // WHILE THERE IS NO TOOL
    public float currentStamina;
    public float maxStamina;

    public StaminaDecrease OnStaminaDecrease = new StaminaDecrease();

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
    public void StaminaDecreased(float p_amount)
    {
        if (currentStamina > 0)
        {
            currentStamina -= p_amount;
        }
  
        OnStaminaDecrease.Invoke(this);
    }
}
