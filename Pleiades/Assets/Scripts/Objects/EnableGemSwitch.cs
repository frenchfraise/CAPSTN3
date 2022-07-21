using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGemSwitch : MonoBehaviour
{
    public int number;
    
    private void OnTriggerEnter2D (Collider2D col)
    {
        GameManager.Instance.gem.GemGet(number);
    }
}
