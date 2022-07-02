using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantOneNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        HerbVariantOneNodePool.pool.Release(this);
    }
}
