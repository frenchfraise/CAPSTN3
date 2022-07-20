using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnFoodUseEvent : UnityEvent<float> { }
public class OnFoodFirstTimeUseEvent : UnityEvent { }
public class Food : MonoBehaviour
{
    public OnFoodUseEvent onFoodUseEvent = new OnFoodUseEvent();
    public static OnFoodFirstTimeUseEvent onFoodFirstTimeUseEvent = new OnFoodFirstTimeUseEvent();

    public float staminaRegen;
    private bool isFirstTime;

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
        if (!isFirstTime)
        {
            onFoodUseEvent.Invoke(staminaRegen);
        }
        else
        {
            TutorialUI.onRemindTutorialEvent.Invoke("food");
        }
    }
    public void FirstTime()
    {
        isFirstTime = false;
        onFoodFirstTimeUseEvent.RemoveListener(FirstTime);
        FoodButton();
    }
}
