using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ResourceNodeHit : UnityEvent<string> { }
public class ResourceNode : MonoBehaviour
{
    public SO_ResourceNode so_ResourceNode;
    public GameObject healthOverheadUIPrefab;
    public Resource resource;


    public ResourceNodeHit OnHit = new ResourceNodeHit();

    private void Awake()
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



    private void Update()
    {
        //FOR TESTING PURPOSES WHILE THERE IS NO TOOL
        if (Input.GetMouseButtonDown(0))
        {
            OnHit.Invoke("Axe");
            Stamina.instance.StaminaDecreased(5); //temporary, the tool will be calling the stamina decrease
        }
    }

    public void Hit(string p_tool) // replace string class to tool class when crates
    {
        if (p_tool == so_ResourceNode.toolRequired)
        {
            //if (p_tool.level >= so_ResourceNode.levelRequirement)
            //{
                Health health = GetComponent<Health>(); //temp
                health.OnDamaged.Invoke(health);
            //}
  

        }
    }

    public void RewardResource()
    {
        InventoryManager.AddResource(resource);
    }

    public void Died()
    {
        
        ObjectPoolManager.instance.pool.Release(this);
    }
}
