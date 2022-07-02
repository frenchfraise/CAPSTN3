using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantThreeNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        BambooVariantThreeNodePool.pool.Release(this);
    }
}
