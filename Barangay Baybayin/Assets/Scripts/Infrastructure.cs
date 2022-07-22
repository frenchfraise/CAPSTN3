using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InfrastructureHitEvent : UnityEvent<int, float, UnityEvent> { }
[RequireComponent(typeof(Health))]
public class Infrastructure : Unit
{
    public bool canInteract = false;
    public Sprite hintSprite;

    [SerializeField] public SO_Infrastructure so_Infrastructure;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] public int currentLevel = 1;
    [SerializeField] public Collider2D col;

    public InfrastructureHitEvent OnInfrastructureHitEvent = new InfrastructureHitEvent();
    protected override void Awake()
    {
        sr = sr ? sr : GetComponent<SpriteRenderer>();
        OnInfrastructureHitEvent.AddListener(Hit);
    }

    protected override void Start()
    {
        canInteract = false; 
        base.Start();
       
        InitializeValues();
    }

    private void OnDestroy()
    {
        OnInfrastructureHitEvent.RemoveListener(Hit);
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
        Health health = GetComponent<Health>();
        health.OnDeathEvent.RemoveListener(Constructed);
        base.DeinitializeValues();
       
    }
   
    protected void Hit(int p_craftLevel, float p_currentDamage, UnityEvent p_eventCallback)
    {
        if (canInteract)
        {
            //if (p_craftLevel >= levelRequirement)
            //{
          //  Debug.Log("HIT");
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
            ToolManager.onResourceNodeFinishedEvent.Invoke();
            //healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
            if (col == null)
            {
                if (currentLevel < so_Infrastructure.boxColliderSize.Count)
                    GetComponent<BoxCollider2D>().size = so_Infrastructure.boxColliderSize[currentLevel];
                if (currentLevel < so_Infrastructure.boxColliderOffSet.Count)
                    GetComponent<BoxCollider2D>().offset = so_Infrastructure.boxColliderOffSet[currentLevel];

            }
            else if(col != null)
            {
                col.enabled = false;
            }
      
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
