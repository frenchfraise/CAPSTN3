using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantTwoNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        HerbVariantTwoNodePool.pool.Release(this);
    }
}
