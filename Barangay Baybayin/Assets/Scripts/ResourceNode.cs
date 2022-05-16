using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

[System.Serializable]
public class ResourceDrop
{
    public SO_Resource so_Resource;
    public float chance;
    public int minAmount;
    public int maxAmount;

}

public class ResourceNodeHit : UnityEvent<Tool> { }
public class ResourceNode : PoolableObject
{
   
    [SerializeField] private HealthOverheadUI healthOverheadUIPrefab;
    private HealthOverheadUI healthOverheadUI;
    public SO_ResourceNode so_ResourceNode;

    public int levelRequirement;

    //public List<ResourceDrop> so_Resources = new List<ResourceDrop>(); //chance


    public ResourceDrop resourceDrop;
    public int rewardAmount;

    public float maxHealth;
    public float regenerationTime;


    public ResourceNodeHit OnHit = new ResourceNodeHit();



    private void Start()
    {
   
        Health health = GetComponent<Health>();
        health.SetValues(maxHealth);

        //OnHit.AddListener(health.OnDamaged);
  
        
        health.enabled = true;
        




    }

   

    private void OnEnable()
    {
        Health health = GetComponent<Health>();
        health.OnDeath.AddListener(RewardResource);
        health.OnDeath.AddListener(Died);

        PoolableObject healthOverheadUIObject = ObjectPoolManager.GetPool(healthOverheadUIPrefab).pool.Get();
        healthOverheadUI = healthOverheadUIObject.GetComponent<HealthOverheadUI>();
        //GameObject healthOverheadUIGO = Instantiate(healthOverheadUIPrefab) as GameObject;
        //HealthOverheadUI healthOverheadUI = healthOverheadUIGO.GetComponent<HealthOverheadUI>();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.test.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeath.AddListener(healthOverheadUI.OnHealthDied);

        OnHit.AddListener(Hit);

        rewardAmount = Random.Range(resourceDrop.minAmount, resourceDrop.maxAmount);

    }

    private void OnDisable()
    {
        Health health = GetComponent<Health>();
        health.OnDeath.RemoveListener(RewardResource);
        health.OnDeath.RemoveListener(Died);

        //GameObject healthOverheadUIGO = Instantiate(healthOverheadUIPrefab) as GameObject;
        //HealthOverheadUI healthOverheadUI = healthOverheadUIGO.GetComponent<HealthOverheadUI>();
      
        health.test.RemoveListener(healthOverheadUI.OnHealthChanged);
        health.OnDeath.RemoveListener(healthOverheadUI.OnHealthDied);

        OnHit.RemoveListener(Hit);

    }

    public void Hit(Tool p_tool) // replace string class to tool class when crates
    {

        if (p_tool.so_Tool.useForResourceNode == so_ResourceNode)
        {
            
            if (p_tool.so_Tool.upgradeLevel >= levelRequirement)
            {
                
                Health health = GetComponent<Health>(); //temp
                health.OnDamaged.Invoke(health);

            }
  

        }
    }

    public void RewardResource()
    {
        InventoryManager.AddResource(resourceDrop.so_Resource, rewardAmount);
    }

    public void Died()
    {
  
        genericObjectPool.pool.Release(this);
    }
}
