using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{

    public HealthBar healthBar;

    void Start()
    {
        maxHp = 100;
        curHp = maxHp;
        atkDmg = 10;
        healthBar.SetMaxHealth(maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player got Attacked!");
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        damage = atkDmg;
        curHp -= damage;

        healthBar.SetHealth(curHp);
    }
}
