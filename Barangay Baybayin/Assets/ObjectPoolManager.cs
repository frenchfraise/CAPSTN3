using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public ResourceNode prefab;
    public ObjectPool<ResourceNode> pool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //LinkedPool unlimited but more effort
        pool = new ObjectPool<ResourceNode>(
            () => //Create Object
            {
                return Instantiate(prefab);
            }
            
            ,
            Tprefab => //Get Object
            {
                Tprefab.gameObject.SetActive(true);
            },
            Tprefab => //Release Object
            {
                Tprefab.gameObject.SetActive(false);
            },
            Tprefab => //Destroy Object
            {
                Destroy(Tprefab.gameObject);
            },
            false //Object Collection
            ,
            10 //Object Min Capacity 
            ,
            20 //Object Max Capacity 
            );
    }

    ResourceNode Create()
    {
        return Instantiate(prefab);
    }


}
