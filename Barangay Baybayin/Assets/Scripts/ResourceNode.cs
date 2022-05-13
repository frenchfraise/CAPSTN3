using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ResourceNodeHit : UnityEvent<Tool> { }
public class ResourceNode : MonoBehaviour
{
    public SO_ResourceNode so_ResourceNode;
    public GameObject healthOverheadUIPrefab;
    public int levelRequirement;

    public int rewardAmount;


    public ResourceNodeHit OnHit = new ResourceNodeHit();

    private void Start()
    {
        Health health = GetComponent<Health>();
        health.SetValues(so_ResourceNode.maxHealth);
        //OnHit.AddListener(health.OnDamaged);
        health.OnDeath.AddListener(RewardResource);
        health.OnDeath.AddListener(Died);
        
        health.enabled = true;

        GameObject healthOverheadUIGO = Instantiate(healthOverheadUIPrefab) as GameObject;
        HealthOverheadUI healthOverheadUI = healthOverheadUIGO.GetComponent<HealthOverheadUI>();
        healthOverheadUI.SetHealthBarData(transform, UIManager.instance.overheadUI);
        health.OnDamaged.AddListener(healthOverheadUI.OnHealthChanged);
        health.OnDeath.AddListener(healthOverheadUI.OnHealthDied);
        healthOverheadUI.transform.SetParent(UIManager.instance.overheadUI, false);

    }

   

    private void OnEnable()
    {
        OnHit.AddListener(Hit);

    }

    public void Hit(Tool p_tool) // replace string class to tool class when crates
    {

        if (p_tool.so_Tool.useForResourceNode == so_ResourceNode)
        {
          
            if (p_tool.so_Tool.level >= levelRequirement)
            {
                Health health = GetComponent<Health>(); //temp
                health.OnDamaged.Invoke(health);

            }
  

        }
    }

    public void RewardResource()
    {
        InventoryManager.AddResource(so_ResourceNode.resource, rewardAmount);
    }

    public void Died()
    {
        
        ObjectPoolManager.instance.pool.Release(this);
    }
}
