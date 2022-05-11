using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Action<GameObject,float> OnDamaged;
    public Action OnDeath;
    public Action<Health> OnHealthUpdated;
    // Start is called before the first frame update
    void Awake()
    {
        HealthBar healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar)
        {
            OnHealthUpdated += healthBar.HealthUpdate;
        }
    
        OnDamaged += TakeDamage;

    }
    public void ValuesSetUp(float p_setHealth, float p_setMaxHealth)
    {
        currentHealth = p_setHealth;
        maxHealth = p_setMaxHealth;
    }
    public void TakeDamage(GameObject p_inflictor, float p_damage)
    {
        AudioManager.Instance.enemyGotHit.Play();
        Debug.Log("took damage");
        
        currentHealth -= p_damage;
        OnHealthUpdated?.Invoke(this);
        CheckIfAlive();
    }

    public void CheckIfAlive()
    {
        if (currentHealth <= 0)
        {
   
            Death();
        }
    }

    public void Death()
    {
        OnDeath?.Invoke();
        //enemyInteract.OnEnemyDeath(this.enemyInteract.indexNo);
        this.gameObject.SetActive(false);
    }

    //void OnDeath(int index)
    //{
    //    interactedWith = true;
    //    PuzzleManager.instance.OnItemInteracted(indexNo);
    //}


}
