using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    
    void Update()
    {
        if(Input.GetKeyDown("a"))
        {

            Debug.Log("Enemy Attacked!");
        }
    }
}
