using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AddFood : UnityEvent<int> { }
public class UpdateFood : UnityEvent<int> { }
public class FirstTimeFood : UnityEvent { }
public class OnFoodUseEvent : UnityEvent<float> { }
public class Food : MonoBehaviour
{
    public OnFoodUseEvent onFoodUseEvent = new OnFoodUseEvent();

    public int amount;
    bool firstTime = true;
    public float staminaRegen;
    private Stamina stamina;
    public static AddFood onAddFood =  new AddFood();
    public static UpdateFood onUpdateFood = new UpdateFood();
    public static FirstTimeFood onFirstTimeFood = new FirstTimeFood();

    private void Awake()
    {
        onFirstTimeFood.AddListener(FirstTime);
        stamina = PlayerManager.instance.playerStamina;
        onFoodUseEvent.AddListener(stamina.IncrementStamina);
        onAddFood.AddListener(AddFood);
        onAddFood.Invoke(2);
    }
    private void OnDestroy()
    {
        onFirstTimeFood.RemoveListener(FirstTime);
        onFoodUseEvent.RemoveListener(stamina.IncrementStamina);
        onAddFood.RemoveListener(AddFood);
    }
    private void FirstTime()
    {
        if (firstTime)
        {
            onFirstTimeFood.RemoveListener(FirstTime);
            firstTime = false;            
            TutorialUI.onRemindTutorialEvent.Invoke(4);
        }
    }
    void AddFood(int p_newAmount)
    {        
        amount += p_newAmount;
        onUpdateFood.Invoke(amount);        
    }
    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
       
    }

    public void FoodButton()
    {
        //Play SFX
        if (amount > 0)
        {
            if (stamina.currentMaxStamina < stamina.currentStamina + staminaRegen)
                Stamina.onManualSetStaminaEvent.Invoke(stamina.currentMaxStamina);
            amount--;
            onUpdateFood.Invoke(amount);
            onFoodUseEvent.Invoke(staminaRegen);
        }
    } 
}
