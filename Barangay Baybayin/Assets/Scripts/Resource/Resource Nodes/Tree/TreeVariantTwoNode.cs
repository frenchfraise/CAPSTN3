using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantTwoNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        TreeVariantTwoNodePool.pool.Release(this);
    }
}
