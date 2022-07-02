using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooVariantOneNode : ResourceNode
{
    protected override void Death()
    {
        base.Death();
        BambooVariantOneNodePool.pool.Release(this);
    }
}
