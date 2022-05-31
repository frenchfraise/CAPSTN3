using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class ResourceNodeHit : UnityEvent<SO_ResourceNode , int , int, UnityEvent > { }
public class ResourceNode : PoolableObject
{
   
    [SerializeField] private HealthOverheadUI healthOverheadUIPrefab;
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

    public ResourceNodeHit OnHit = new ResourceNodeHit();

    private void Start()
    {
        Health health = GetComponent<Health>();
        health.SetValues(maxHealth);
        health.enabled = true;
        
    }

   

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        health.OnDeath.AddListener(RewardResource);
        health.OnDeath.AddListener(Died);

        PoolableObject healthOverheadUIObject = ObjectPoolManager.GetPool(healthOverheadUIPrefab).pool.Get();
        healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();
  
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.onHealthModified.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeath.AddListener(healthOverheadUI.OnHealthDied);

        OnHit.AddListener(Hit);
    }

    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        health.OnDeath.RemoveListener(RewardResource);
        health.OnDeath.RemoveListener(Died);

        health.onHealthModified.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeath.RemoveListener(healthOverheadUI.OnHealthDied);

        OnHit.RemoveListener(Hit);
    }

    public void Hit( SO_ResourceNode p_useForResourceNode,int p_craftLevel, int p_currentDamage, UnityEvent p_functionCallback) 
    {
        //Debug.Log("1 " + p_useForResourceNode + " - " + p_craftLevel + " - " + p_currentDamage + " - ");
        if (p_useForResourceNode == so_ResourceNode)
        {
            
            if (p_craftLevel >= levelRequirement)
            {
                
                Health health = GetComponent<Health>();         
     
                health.onHealthModify.Invoke(-p_currentDamage);                
          
                p_functionCallback.Invoke();
        
            }
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
