using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantTwoNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        HerbVariantTwoNodePool.pool.Release(this);
    }
}
