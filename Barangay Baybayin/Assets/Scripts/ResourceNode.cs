using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ResourceNodeHitEvent : UnityEvent<SO_ResourceNode , int , int, UnityEvent > { }
public class ResourceNode : PoolableObject
{
   
    private HealthOverheadUI healthOverheadUI;
    public SO_ResourceNode so_ResourceNode;

    [Header("Item")]
    public Item itemPrefab;



    public int levelRequirement;

    public List<ResourceDrop> resourceDrops = new List<ResourceDrop>(); //chance


    //public ResourceDrop resourceDrop;
    public int rewardAmount;

    public float maxHealth;
    public float regenerationTime;

    public ResourceNodeHitEvent OnResourceNodeHitEvent = new ResourceNodeHitEvent();

    private void Start()
    {
        Health health = GetComponent<Health>();
        health.SetValues(maxHealth);
        health.enabled = true;
        
    }

   

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        health.OnDeathEvent.AddListener(RewardResource);
        health.OnDeathEvent.AddListener(Died);
        GenericObjectPool objectPool = ObjectPoolManager.GetPool(typeof(HealthOverheadUI));
        PoolableObject healthOverheadUIObject = objectPool.pool.Get();
        healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();
  
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.onHealthModifiedEvent.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.AddListener(healthOverheadUI.OnHealthDied);

        OnResourceNodeHitEvent.AddListener(Hit);
    }

    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        health.OnDeathEvent.RemoveListener(RewardResource);
        health.OnDeathEvent.RemoveListener(Died);

        health.onHealthModifiedEvent.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeathEvent.RemoveListener(healthOverheadUI.OnHealthDied);

        OnResourceNodeHitEvent.RemoveListener(Hit);
    }

    public void Hit( SO_ResourceNode p_useForResourceNode,int p_craftLevel, int p_currentDamage, UnityEvent p_eventCallback) 
    {
        //Debug.Log("1 " + p_useForResourceNode + " - " + p_craftLevel + " - " + p_currentDamage + " - ");
        if (p_useForResourceNode == so_ResourceNode)
        {
            
            //if (p_craftLevel >= levelRequirement)
            //{
                
                Health health = GetComponent<Health>();         
     
                health.onHealthModifyEvent.Invoke(-p_currentDamage);                
          
                p_eventCallback.Invoke();
        
            //}
        }
    }

    public void RewardResource()
    {
        int chosenIndex = Random.Range(0, resourceDrops.Count);
        
        ResourceDrop chosenResourceDrop = resourceDrops[chosenIndex];
        rewardAmount = Random.Range(chosenResourceDrop.minAmount, chosenResourceDrop.maxAmount);
        for (int i=0; i<rewardAmount; i++)
        {
            Item newItem = Instantiate(itemPrefab);
            
            newItem.transform.position = (Vector2) transform.position;
            newItem.startPosition = (Vector2) transform.position;
            newItem.so_Item = chosenResourceDrop.so_Item;
            newItem.GetComponent<SpriteRenderer>().sprite = chosenResourceDrop.so_Item.icon;
        }
        
    }

    public void Died()
    {
        genericObjectPool.pool.Release(this);
    }
}
