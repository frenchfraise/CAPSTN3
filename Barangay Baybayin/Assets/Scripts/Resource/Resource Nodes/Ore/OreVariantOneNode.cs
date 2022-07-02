using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantOneNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        OreVariantOneNodePool.pool.Release(this);
    }
}
