using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantThreeNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        NipaLeavesVariantThreeNodePool.pool.Release(this);
    }
}
