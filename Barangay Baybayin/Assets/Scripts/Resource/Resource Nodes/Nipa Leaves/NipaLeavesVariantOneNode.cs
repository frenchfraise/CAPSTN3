using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipaLeavesVariantOneNode : ResourceNode
{
   

    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        NipaLeavesVariantOneNodePool.pool.Release(this);
    }
}
