using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class GenericObjectPool : MonoBehaviour
{
    [SerializeField]
    private bool isAlwaysInContainer;
    [SerializeField]
    private bool isInactiveInContainer;
    [SerializeField] private Transform container;
    public PoolableObject prefab;
    public ObjectPool<PoolableObject> pool;
    [SerializeField]
    private bool isCollectionCheck;
    [SerializeField]
    private int defaultMaxAmount;
    [SerializeField]
    private int flexibleMaxAmount;
    private void Awake()
    {

        pool = new ObjectPool<PoolableObject>(
            CreateObject,
            GetObject,
            ReleaseObject,
            DestroyObject,
            isCollectionCheck
            ,
            defaultMaxAmount
            ,
            flexibleMaxAmount
            );
    }

    PoolableObject CreateObject()
    {
        var newObject = Instantiate(prefab);
        newObject.gameObject.name = prefab.name + pool.CountActive.ToString(); //Temporary, for tracking purposes
        if (isAlwaysInContainer)
        {
            if (container != null)
            {
            
                newObject.transform.SetParent(container);
            }
        }

        var newPoolableObject = newObject.gameObject.GetComponent<PoolableObject>();
        newPoolableObject.SetPool(this);
        
        return newPoolableObject;
    }

    void GetObject(PoolableObject p_desiredObject)
    {
        if (!isInactiveInContainer)
        {
            p_desiredObject.gameObject.SetActive(true);
        }
    }

    void ReleaseObject(PoolableObject p_desiredObject)
    {
        if (!isInactiveInContainer)
        {
            p_desiredObject.gameObject.SetActive(false);
        }
        
        
    }

    void DestroyObject(PoolableObject p_desiredObject)
    {
        Destroy(p_desiredObject.gameObject);
    }
}
