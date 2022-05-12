using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    public EnemyScriptableObject enemyStats;

    public GameObject healthGem;
    void Awake()
    {
        //Init(enemyStats.currentHealth, enemyStats.maxHealth, enemyStats.damage);
        Health healthComponent = GetComponent<Health>();
        healthComponent.ValuesSetUp(enemyStats.currentHealth, enemyStats.maxHealth);
        healthComponent.OnDeath += Death;

        currentElementalType = GetComponent<ElementalType>();
        if (currentElementalType)
        {
            currentElementalType.elementalType = enemyStats.elementalType;
        }
  

    }
 

    

    public void HealthChance()
    {
        int chance = Random.Range(0, 100);
        Debug.Log("chance: " + chance);
        if (chance <= 50)
        {
            Instantiate(healthGem, transform.position, Quaternion.identity);
        }
    }

    public void Death()
    {

    }
}
