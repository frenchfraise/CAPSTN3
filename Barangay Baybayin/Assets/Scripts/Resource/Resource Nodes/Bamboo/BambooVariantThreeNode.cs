using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantThreeNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        BambooVariantThreeNodePool.pool.Release(this);
    }
}
