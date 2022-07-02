using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantThreeNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        TreeVariantThreeNodePool.pool.Release(this);
    }
}
