using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnFoodUseEvent : UnityEvent<float> { }
public class Food : MonoBehaviour
{
    public OnFoodUseEvent onFoodUseEvent = new OnFoodUseEvent();


    public float staminaRegen;


    private void OnEnable()
    {
        if (TryGetComponent(out Stamina stamina))
        {
            onFoodUseEvent.AddListener(stamina.IncrementStamina);
        }
    }

    private void OnDisable()
    {
        if(TryGetComponent(out Stamina stamina))
        {
            onFoodUseEvent.AddListener(stamina.IncrementStamina);
        }
    }

    public void FoodButton()
    {
        
        onFoodUseEvent.Invoke(staminaRegen);
       
    }
 
}
