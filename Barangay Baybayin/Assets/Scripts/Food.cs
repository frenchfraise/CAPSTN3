using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AddFood : UnityEvent<int> { }
public class UpdateFood : UnityEvent<int> { }
public class OnFoodUseEvent : UnityEvent<float> { }
public class Food : MonoBehaviour
{
    public OnFoodUseEvent onFoodUseEvent = new OnFoodUseEvent();

    public int amount;
    public float staminaRegen;
    public static AddFood onAddFood =  new AddFood();
    public static UpdateFood onUpdateFood = new UpdateFood();
    void AddFood(int p_newAmount)
    {
        amount += p_newAmount;
        onUpdateFood.Invoke(amount);
    }
    private void OnEnable()
    {
        if (TryGetComponent(out Stamina stamina))
        {
            onFoodUseEvent.AddListener(stamina.IncrementStamina);
        }
        onAddFood.AddListener(AddFood);

    }

    private void OnDisable()
    {
        if(TryGetComponent(out Stamina stamina))
        {
            onFoodUseEvent.RemoveListener(stamina.IncrementStamina);
        }
        onAddFood.RemoveListener(AddFood);
    }

    public void FoodButton()
    {
        if (amount > 0 )
        {
            amount--;
            onUpdateFood.Invoke(amount);
            onFoodUseEvent.Invoke(staminaRegen);

        }

    }
 
}
