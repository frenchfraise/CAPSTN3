using System.Collections;
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
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.playerHit.Play();
        curHp -= damage;

        healthBar.SetHealth(curHp);

        if(curHp <= 0)
        {
            OnDeath();
        }
    }

    public void AddHealth(int value)
    {
        curHp += value;
        healthBar.SetHealth(curHp);
        if (curHp >= 100)
        {
            curHp = 100;
        }

    }

    public void OnDeath()
    {
        RespawnWaypoint.Instance.Respawn(RespawnWaypoint.Instance.currentWaypoint);

        //show Retry, Exit panel
    }
}
