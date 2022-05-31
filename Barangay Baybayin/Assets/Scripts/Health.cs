using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthModify : UnityEvent<float> { }

[System.Serializable]
public class HealthModified : UnityEvent<bool, float, float> { }

[System.Serializable]
public class Died : UnityEvent { }
public class Health : MonoBehaviour
{
    [HideInInspector] public HealthOverheadUI healthOverheadUI;
    private bool isAlive;
    private float currentHealth;
    private float maxHealth;

    
    public HealthModify onHealthModify = new HealthModify();
    public HealthModified onHealthModified = new HealthModified();

    public Died OnDeath = new Died();
    private void OnEnable()
    {
        InitializeValues();
        onHealthModify.AddListener(ModifyHealth);
        //OnDeath.AddListener(Death);
    }


    private void OnDisable()
    {


        onHealthModify.RemoveListener(ModifyHealth);
        //OnDeath.AddListener(Death);
        
    }
    public void SetValues(float p_maxHealth)
    {
        maxHealth = p_maxHealth;
    }
    public void InitializeValues()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void ModifyHealth(float p_modifier)
    {
        currentHealth += p_modifier;
        onHealthModified.Invoke(isAlive,currentHealth,maxHealth);
        CheckHealth();
    }
   

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Death();
            
            OnDeath.Invoke();
        }
    }

    public void Death()
    {
        isAlive = false;
    }
}
