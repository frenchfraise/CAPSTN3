using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantTwoNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        TreeVariantTwoNodePool.pool.Release(this);
    }
}
