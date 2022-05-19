using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Resource> resources = new List<Resource>();

    private void Awake()
    {
        instance = this;
    }

    public static void AddResource(SO_Resource p_resource, int p_amount)
    {
        
        for (int i =0; i < InventoryManager.instance.resources.Count;)
        {
            if (InventoryManager.instance.resources[i].so_Resource == p_resource)
            {
               
                InventoryManager.instance.resources[i].amount += p_amount;
                InventoryManager.instance.resources[i].UpdateText(); //temporary
                break;
            }
            i++;
            if (i >= InventoryManager.instance.resources.Count)
            {
                //Loop finished but didnt find any matching resources
                Debug.Log("FAILED TO ADD RESOURCE " + p_resource.name + " BECAUSE COULD NOT FIND RESOURCE IN INVENTORY WITH MATCHING NAME");
            }
        }
     
    }
}
