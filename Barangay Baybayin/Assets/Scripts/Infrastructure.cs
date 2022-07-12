using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InfrastructureHitEvent : UnityEvent<int, int, UnityEvent> { }
[RequireComponent(typeof(Health))]
public class Infrastructure : Unit
{
    bool canInteract = true;
    public Sprite hintSprite;

    [SerializeField] public SO_Infrastructure so_Infrastructure;

    private SpriteRenderer sr;
    [SerializeField] public int currentLevel = 1;

    public InfrastructureHitEvent OnInfrastructureHitEvent = new InfrastructureHitEvent();
    protected override void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }

    protected override void Start()
    {
        base.Start();
        OnInfrastructureHitEvent.AddListener(Hit);
        InitializeValues();
    }

    public override void InitializeValues()
    {
     
        maxHealth = so_Infrastructure.maxHealth;
        base.InitializeValues();

        Health health = GetComponent<Health>();
        health.OnDeathEvent.AddListener(Constructed);
      
    }
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
    }
   
    protected void Hit(int p_craftLevel, int p_currentDamage, UnityEvent p_eventCallback)
    {
        if (canInteract)
        {
            //if (p_craftLevel >= levelRequirement)
            //{
            Debug.Log("HIT");
            Health health = GetComponent<Health>();

            health.onHealthModifyEvent.Invoke(-p_currentDamage);

            p_eventCallback.Invoke();

            //}
  
        }

    }

    void Constructed()
    {
        
        if (currentLevel >= so_Infrastructure.sprites.Count-1)
        {
            Debug.Log("MAX LEVEL REACHED");
        }
        else
        {
            currentLevel++;
            //currentHealth = 0;

            //healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
            sr.sprite = so_Infrastructure.sprites[currentLevel];
            InitializeValues();
        }


        
    }
    //protected override void OnInteract()
    //{
    //    if (canInteract)
    //    {

    //        currentHealth++;

    //        if (currentHealth >= so_Infrastructure.maxHealth)
    //        {
    //            if (currentLevel >= so_Infrastructure.sprites.Count)
    //            {
    //                Debug.Log("MAX LEVEL REACHED");
    //            }
    //            else
    //            {
    //                currentLevel++;
    //                currentHealth = 0;
    //                sr.sprite = so_Infrastructure.sprites[currentLevel-1];
    //            }


    //        }


    //    }

    //}
}
