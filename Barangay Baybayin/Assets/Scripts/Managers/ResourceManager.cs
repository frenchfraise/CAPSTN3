using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class RespawnResources : UnityEvent { }
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public RespawnResources OnRespawn = new RespawnResources();
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        StartCoroutine(Co_Test());

    }

    IEnumerator Co_Test()
    {

        yield return new WaitForSeconds(1);
    
        ResourceManager.instance.OnRespawn.Invoke();
    }
}
