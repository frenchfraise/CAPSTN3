using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantThreeNode : ResourceNode
{

    protected override void Death()
    {
        base.Death();
        HerbVariantThreeNodePool.pool.Release(this);
    }
}
