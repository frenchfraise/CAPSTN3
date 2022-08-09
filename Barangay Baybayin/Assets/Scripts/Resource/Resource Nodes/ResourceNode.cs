using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using DG.Tweening;
using System;
public class ResourceNodeHitEvent : UnityEvent<List<SO_ResourceNode> , int , float, UnityEvent > { }
public class ResourceNode : Unit
{
 
    public SO_ResourceNode so_ResourceNode;

    public Sprite hintSprite;

    public int levelRequirement;

    [NonReorderable] public List<ResourceDrop> resourceDrops = new List<ResourceDrop>(); //chance

    public ResourceNodeHitEvent OnResourceNodeHitEvent = new ResourceNodeHitEvent();

    float shakePositionDuration = 0.15f;
    Vector3 shakePositionPower = new Vector3(0.5f, 0.5f);
    int shakePositionVibrato = 4;
    float shakePositionRandomRange = 1f;
    bool shakePositionCanFade = false;

    protected override void OnEnable()
    {
        Health health = GetComponent<Health>();
        //health.OnDeathEvent.AddListener(RewardResource);
 
        OnResourceNodeHitEvent.AddListener(Hit);
    }

    protected override void OnDisable()
    {
        Health health = GetComponent<Health>();
        //health.OnDeathEvent.RemoveListener(RewardResource);
   
        OnResourceNodeHitEvent.RemoveListener(Hit);
    }

    public virtual void Hit( List<SO_ResourceNode> p_useForResourceNode,int p_craftLevel, float p_currentDamage, UnityEvent p_eventCallback) 
    {

        if (health.healthOverheadUI == null)
        {
            health.healthOverheadUI = HealthOverheadUIPool.pool.Get();

            health.healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
            health.healthOverheadUI.health = health;
            health.onHealthModifiedEvent.AddListener(health.healthOverheadUI.OnHealthChanged);
            health.OnDeathEvent.AddListener(health.healthOverheadUI.OnHealthDied);
            
        }
        
        foreach (SO_ResourceNode useForResourceNode in p_useForResourceNode)
        {
            if (useForResourceNode == so_ResourceNode)
            {

              

                health.onHealthModifyEvent.Invoke(-p_currentDamage);

                p_eventCallback.Invoke();
                transform.DOShakePosition(shakePositionDuration, shakePositionPower, shakePositionVibrato, shakePositionRandomRange, shakePositionCanFade);
               
            }
        }
    
    }
  
   

    public override void InitializeValues()
    {
        maxHealth = so_ResourceNode.maxHealth;
        base.InitializeValues();

    }

    protected override void Death()
    {
        //List<ResourceDrop> res = new List<ResourceDrop>(resourceDrops); //chance
        //PlayerManager.instance.playerNodePosition = transform.position;
        PlayerManager.instance.playerNodePosition = transform.position;
        PlayerManager.instance.RewardResource(resourceDrops);
        //base.Death();
        base.Death();
       // StartCoroutine(Test(base.Death));
    }

    //IEnumerator Test(Action p_a)
    //{
     
    //    yield return new WaitForSeconds(5f);
    //    p_a.Invoke();
    //}

   
}
