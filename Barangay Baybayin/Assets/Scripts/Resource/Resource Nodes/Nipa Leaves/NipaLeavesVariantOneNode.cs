using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantOneNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        NipaLeavesVariantOneNodePool.pool.Release(this);
    }
}
