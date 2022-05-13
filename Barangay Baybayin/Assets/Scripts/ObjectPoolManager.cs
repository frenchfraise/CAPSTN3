using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    [SerializeField] private ResourceNode prefab;
    public ObjectPool<ResourceNode> pool;
    [SerializeField]
    private bool isObjectCollecting;
    [SerializeField]
    private int minAmount;
    [SerializeField]
    private int maxAmount;
    private void Awake()
    {
        instance = this;
        pool = new ObjectPool<ResourceNode>(
            CreateObject,
            GetObject,
            ReleaseObject,
            DestroyObject,
            isObjectCollecting //Object Collection
            ,
            minAmount //Object Min Capacity 
            ,
            maxAmount //Object Max Capacity 
            );
    }

    ResourceNode CreateObject()
    {
        return Instantiate(prefab);
    }

    void GetObject(ResourceNode p_desiredObject)
    {
        p_desiredObject.gameObject.SetActive(true);
    }

    void ReleaseObject(ResourceNode p_desiredObject)
    {
        p_desiredObject.gameObject.SetActive(false);
    }

    void DestroyObject(ResourceNode p_desiredObject)
    {
        Destroy(p_desiredObject.gameObject);
    }

}
