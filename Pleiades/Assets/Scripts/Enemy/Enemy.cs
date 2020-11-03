using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public HealthBar healthBar;
    void Start()
    {
        Init();
    }

    void Update()
    {
        if(curHp <= 0)
        {
            Death();
        }
    }

    void Init()
    {
        this.curHp = 100;
        this.maxHp = 100;
        this.damage = 5;
        healthBar.SetMaxHealth(maxHp);
    }

    public void TakeDamage()
    {
        Debug.Log("took damage");
        int damage = GameManager.Instance.player.atkDmg;
        this.curHp -= damage;
        healthBar.SetHealth(curHp);
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
    }
}
