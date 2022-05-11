using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damaged : UnityEvent<Health> { }
public class Died : UnityEvent { }
public class Health : MonoBehaviour
{
    public bool isAlive;
    public float currentHealth;
    public float maxHealth;


    public Damaged OnDamaged = new Damaged();
    public Died OnDeath = new Died();
    private void OnEnable()
    {
        InitializeValues();
        //OnDamaged.AddListener(CheckHealth);
        //OnDeath.AddListener(Death);
    }
    public void InitializeValues()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }

    public void Damaged()
    {
        OnDamaged.Invoke(this);
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
