using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantTwoNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        BambooVariantTwoNodePool.pool.Release(this);
    }
}
