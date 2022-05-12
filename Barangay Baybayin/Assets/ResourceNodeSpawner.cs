using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeSpawner : MonoBehaviour
{
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
     
        ResourceNode newResource =  ObjectPoolManager.instance.pool.Get(); //temporary, will make this generic so any class can use next time
        newResource.transform.position = transform.position;
    }
}
