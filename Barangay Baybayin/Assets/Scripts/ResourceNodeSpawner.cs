using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
    public List<ResourceNode> resourceNodes = new List<ResourceNode>();
    private void Start()
    {

        ResourceManager.instance.OnRespawn.AddListener(Spawn);
    }

    private void OnDestroy()
    {

        ResourceManager.instance.OnRespawn.RemoveListener(Spawn);
    }

    void Spawn()
    {
        int chosenIndex = Random.Range(0,resourceNodes.Count);
        PoolableObject chosenResourceNode = resourceNodes[chosenIndex];
        PoolableObject newResourceNode = ObjectPoolManager.GetPool(chosenResourceNode).pool.Get(); 
        newResourceNode.transform.position = transform.position;
    }


}
