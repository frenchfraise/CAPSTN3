using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantThreeNode : ResourceNode
{

    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        HerbVariantThreeNodePool.pool.Release(this);
    }
}
