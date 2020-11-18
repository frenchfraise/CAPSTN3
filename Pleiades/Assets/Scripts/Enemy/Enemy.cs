using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    private EnemyInteract enemyInteract;

    public HealthBar healthBar;

    void Start()
    {
        enemyInteract = this.GetComponent<EnemyInteract>();
    }

    void Update()
    {
        if(curHp <= 0)
        {
            Death();
        }
    }

    public void Init(float n_CurHp, float n_MaxHp, int n_Damage)
    {
        this.curHp = n_CurHp;
        this.maxHp = n_MaxHp;
        this.damage = n_Damage;
        healthBar.SetMaxHealth(n_MaxHp);
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
        enemyInteract.OnEnemyDeath(this.enemyInteract.indexNo);
        this.gameObject.SetActive(false);
    }
}
