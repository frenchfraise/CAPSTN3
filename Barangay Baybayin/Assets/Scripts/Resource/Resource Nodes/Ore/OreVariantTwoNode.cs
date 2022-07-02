using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantTwoNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        OreVariantTwoNodePool.pool.Release(this);
    }
}
