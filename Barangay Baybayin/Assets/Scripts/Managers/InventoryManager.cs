using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private static InventoryManager _instance;
    public static InventoryManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryManager>();
            }

            return _instance;
        }
    }
    public List<ResourceCategory> resourceCategory = new List<ResourceCategory>();
    private void Awake()
    {
        _instance = this;
    }
    public void OnEnable()
    {
        UIManager.instance.inventoryUI.BuildInventory();
    }
    public static Resource GetResource(SO_Resource p_resource)
    {

        for (int ii = 0; ii < InventoryManager.instance.resourceCategory.Count; ii++)
        {

            for (int i = 0; i < InventoryManager.instance.resourceCategory[ii].resources.Count;)
            {
                if (InventoryManager.instance.resourceCategory[ii].resources[i].so_Resource == p_resource)
                {



                    return InventoryManager.instance.resourceCategory[ii].resources[i];
                }
                i++;
                if (i >= InventoryManager.instance.resourceCategory.Count)
                {
                    //Loop finished but didnt find any matching resources
                    Debug.Log("FAILED TO FIND RESOURCE " + p_resource.name + " BECAUSE COULD NOT FIND SO_Resource IN INVENTORY'S RESOURCE CATEGORY'S RESOURCE WITH MATCHING NAME");
                }
            }



        }
        return null;

    }
    public static void AddResource(SO_Resource p_resource, int p_amount)
    {
        
        for (int ii = 0; ii < InventoryManager.instance.resourceCategory.Count; ii++ )
        {
            
            for (int i = 0; i < InventoryManager.instance.resourceCategory[ii].resources.Count;)
            {
                if (InventoryManager.instance.resourceCategory[ii].resources[i].so_Resource == p_resource)
                {

                    InventoryManager.instance.resourceCategory[ii].resources[i].amount += p_amount;
                    InventoryManager.instance.resourceCategory[ii].resources[i].UpdateText(); //temporary
                      
                    return;
                }
                i++;
                if (i >= InventoryManager.instance.resourceCategory.Count)
                {
                    //Loop finished but didnt find any matching resources
                    Debug.Log("FAILED TO ADD RESOURCE " + p_resource.name + " BECAUSE COULD NOT FIND RESOURCE IN INVENTORY WITH MATCHING NAME");
                }
            }
          
           
            
        }
        
     
    }
}
