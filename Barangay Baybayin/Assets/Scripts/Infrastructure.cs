using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InfrastructureHitEvent : UnityEvent<int, int, UnityEvent> { }
[RequireComponent(typeof(Health))]
public class Infrastructure : Unit
{
    public bool canInteract = false;
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
        canInteract = false; 
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
    public void ForQuest()
    {
        canInteract = true;
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
            if (currentLevel < so_Infrastructure.boxColliderSize.Count)
                GetComponent<BoxCollider2D>().size = so_Infrastructure.boxColliderSize[currentLevel];
            if (currentLevel < so_Infrastructure.boxColliderOffSet.Count)
                GetComponent<BoxCollider2D>().offset = so_Infrastructure.boxColliderOffSet[currentLevel];
            sr.sprite = so_Infrastructure.sprites[currentLevel];
            canInteract = false;
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
