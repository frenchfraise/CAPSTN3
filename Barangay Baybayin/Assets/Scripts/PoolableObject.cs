using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class PoolableObject : MonoBehaviour
{
    protected GenericObjectPool genericObjectPool;

    public void SetPool(GenericObjectPool p_pool)
    {
        genericObjectPool = p_pool;
    }
}
