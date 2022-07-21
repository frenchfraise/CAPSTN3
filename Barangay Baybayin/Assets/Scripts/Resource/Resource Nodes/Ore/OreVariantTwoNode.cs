using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantTwoNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        OreVariantTwoNodePool.pool.Release(this);
    }
}
