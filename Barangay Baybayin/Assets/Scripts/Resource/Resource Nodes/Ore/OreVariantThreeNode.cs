using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantThreeNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        OreVariantThreeNodePool.pool.Release(this);
    }
}
