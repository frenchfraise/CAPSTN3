using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [Header("Player Stamina Values")]
    public int maxStamina = 100;
    public int currentStamina;

    [Header("Sliders")]
    public Stamina stamina;

    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        stamina.SetMaxStamina(maxStamina);
    }    

    void DecrementStamina(int value)
    {
        currentStamina -= value;
        stamina.SetStamina(currentStamina);
    }    
}
