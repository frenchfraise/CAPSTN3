using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantOneNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        BambooVariantOneNodePool.pool.Release(this);
    }
}
