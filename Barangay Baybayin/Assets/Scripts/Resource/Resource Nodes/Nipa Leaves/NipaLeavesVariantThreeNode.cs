using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantThreeNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        NipaLeavesVariantThreeNodePool.pool.Release(this);
    }
}
