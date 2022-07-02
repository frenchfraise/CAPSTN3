using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantTwoNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        NipaLeavesVariantTwoNodePool.pool.Release(this);
    }
}
