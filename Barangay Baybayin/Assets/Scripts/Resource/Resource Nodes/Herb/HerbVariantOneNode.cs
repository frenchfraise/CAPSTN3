using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbVariantOneNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        HerbVariantOneNodePool.pool.Release(this);
    }
}
