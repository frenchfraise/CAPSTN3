using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Health health;

    protected float maxHealth;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        Health health = GetComponent<Health>();
        
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
       
        health = GetComponent<Health>();
        health.OnDeathEvent.AddListener(Death);
        health.SetValues(maxHealth);
        health.InitializeValues();
     

    

        //Debug.Log(gameObject.name + " - " + GetInstanceID() + " INITIALIZED " + healthOverheadUI);

    }

  

    public virtual void DeinitializeValues()
    {
        //Debug.Log(gameObject.name + " DEINITIALIZED");
      
        health.OnDeathEvent.RemoveListener(Death);
        if (health.healthOverheadUI != null)
        {
            health.healthOverheadUI.Deinit();
            health.healthOverheadUI = null;
        }




    }

    protected virtual void Death()
    {
        DeinitializeValues();
        
    }
}
