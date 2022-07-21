using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreVariantOneNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        OreVariantOneNodePool.pool.Release(this);
    }
}
