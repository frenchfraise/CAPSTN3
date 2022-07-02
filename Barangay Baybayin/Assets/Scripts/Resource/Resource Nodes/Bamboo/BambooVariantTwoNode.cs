using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantTwoNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        BambooVariantTwoNodePool.pool.Release(this);
    }
}
