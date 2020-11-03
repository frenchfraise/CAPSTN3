﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{

    public HealthBar healthBar;

    void Start()
    {
        Init();
    }

    void Init()
    {
        this.maxHp = 100;
        this.curHp = 100;
        this.atkDmg = 50;
        healthBar.SetMaxHealth(maxHp);
    }
    
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
