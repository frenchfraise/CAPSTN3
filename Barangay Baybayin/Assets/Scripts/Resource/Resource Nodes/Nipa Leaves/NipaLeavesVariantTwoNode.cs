using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantTwoNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        NipaLeavesVariantTwoNodePool.pool.Release(this);
    }
}
