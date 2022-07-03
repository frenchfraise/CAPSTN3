using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantThreeNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        TreeVariantThreeNodePool.pool.Release(this);
    }
}
