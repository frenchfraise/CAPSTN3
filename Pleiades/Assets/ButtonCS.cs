using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCS : MonoBehaviour
{
    [Header("Variables")]
    public Player2 player;

    void DecrementStamina(int value)
    {
        player.currentStamina -= value;
        player.stamina.SetStamina(player.currentStamina);
    }

    #region Buttons
    public void OnUseToolButtonClick()
    {
        // Debug.Log("Stamina: " + player.currentStamina);
        DecrementStamina(20);
    }
    public void OnRefillStaminaButtonClick()
    {
        player.currentStamina = player.maxStamina;
        player.stamina.SetStamina(player.maxStamina);
    }
    
    public void OnToolOne()
    {
        Debug.Log("Tool #1 equipped!");
    }

    public void OnToolTwo()
    {
        Debug.Log("Tool #2 equipped!");
    }

    public void OnToolThree()
    {
        Debug.Log("Tool #3 equipped!");
    }

    public void OnToolFour()
    {
        Debug.Log("Tool #4 equipped!");
    }
    #endregion
}
