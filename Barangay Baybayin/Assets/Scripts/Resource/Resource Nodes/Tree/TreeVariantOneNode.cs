using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantOneNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        TreeVariantOneNodePool.pool.Release(this); 
    }
}
