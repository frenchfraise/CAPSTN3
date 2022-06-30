using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;
    public static ObjectPoolManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPoolManager>();
            }

            return _instance;

        }
    }
    //[HideInInspector] 
    public List<GenericObjectPool> pools = new List<GenericObjectPool>();
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
           
        }
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

    public static GenericObjectPool GetPool(System.Type p_type)
    {

        foreach (GenericObjectPool pool in ObjectPoolManager.instance.pools)
        {
            if (pool.prefab.GetType() == p_type)
            {
                return pool;
            }
        }
        return null;
    }
}
