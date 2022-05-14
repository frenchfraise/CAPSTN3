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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Co_Test());
    }

    IEnumerator Co_Test()
    {
        yield return new WaitForSeconds(1);

        OnRespawn.Invoke();
        yield return new WaitForSeconds(30f);
        OnRespawn.Invoke();
    }
   
}
