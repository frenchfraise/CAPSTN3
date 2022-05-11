using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceNodeHit : UnityEvent { }
public class ResourceNode : MonoBehaviour
{
    public SO_ResourceNode SO_resourceNode;

    public string toolRequired; // change this class to tool when Emman made it already
    public float levelRequirement;
    public float staminaCost;

    public ResourceNodeHit OnHit;

    
    public void Reward(int p_inventory)
    {

        p_inventory += SO_resourceNode.resouceAmount;
    }
}
