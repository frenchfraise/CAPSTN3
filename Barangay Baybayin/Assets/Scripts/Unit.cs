using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public HealthOverheadUI healthOverheadUI;
    public float maxHealth;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        Health health = GetComponent<Health>();
        health.SetValues(maxHealth);
        health.enabled = true;
        //InitializeValues();

    }
    protected virtual void OnEnable()
    {
      
    }

    protected virtual void OnDisable()
    {
     
    }
    public virtual void InitializeValues()
    {
       
        Health health = GetComponent<Health>();
        health.OnDeathEvent.AddListener(Death);
        healthOverheadUI = HealthOverheadUIPool.pool.Get();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.onHealthModifiedEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);
        //Debug.Log(gameObject.name + " - " + GetInstanceID() + " INITIALIZED " + healthOverheadUI);

    }

  

    public virtual void DeinitializeValues()
    {
        //Debug.Log(gameObject.name + " DEINITIALIZED");
        Health health = GetComponent<Health>();
        health.OnDeathEvent.RemoveListener(Death);
        
        health.onHealthModifiedEvent.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.RemoveListener(healthOverheadUI.OnHealthDied);

        HealthOverheadUIPool.pool.Release(healthOverheadUI);
        healthOverheadUI = null;
    }

    protected virtual void Death()
    {
        DeinitializeValues();
    
    }
}
