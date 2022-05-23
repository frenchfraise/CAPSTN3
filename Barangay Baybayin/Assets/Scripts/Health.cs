using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Damaged : UnityEvent<Health, int> { }

[System.Serializable]
public class Test : UnityEvent<Health> { }

[System.Serializable]
public class Died : UnityEvent { }
public class Health : MonoBehaviour
{
    public bool isAlive;
    public float currentHealth;
    public float maxHealth;

    public Test test = new Test();
    public Damaged OnDamaged = new Damaged();
    public Died OnDeath = new Died();
    private void OnEnable()
    {
        InitializeValues();
        OnDamaged.AddListener(Damaged);
        //OnDeath.AddListener(Death);
    }


    private void OnDisable()
    {
        

        OnDamaged.RemoveListener(Damaged);
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

    public void Damaged(Health p_health, int value)
    {
        currentHealth -= value;
        test.Invoke(this);
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
