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
        int chosenResourceNode = Random.Range(0,resourceNodes.Count);
        //resourceNodes[chosenResourceNode]
        ResourceNode newResource =  ObjectPoolManager.instance.pool.Get(); //temporary, will make this generic so any class can use next time
        newResource.transform.position = transform.position;
    }
}
