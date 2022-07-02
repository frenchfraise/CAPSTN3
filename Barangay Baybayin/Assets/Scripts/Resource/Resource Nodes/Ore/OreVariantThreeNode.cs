using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantThreeNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        OreVariantThreeNodePool.pool.Release(this);
    }
}
