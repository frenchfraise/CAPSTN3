using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;
    public List<GenericObjectPool> pools = new List<GenericObjectPool>();
    private void Awake()
    {
        instance = this;
        foreach (GenericObjectPool pool in transform.GetComponentsInChildren(typeof(GenericObjectPool)))
        {
            pools.Add(pool);
        }
    }

    public static GenericObjectPool GetPool(PoolableObject p_prefab)
    {
        
        foreach (GenericObjectPool pool in ObjectPoolManager.instance.pools)
        {
            if (pool.prefab == p_prefab)
            {
                return pool;
            }
        }
        return null; 
    }


}
