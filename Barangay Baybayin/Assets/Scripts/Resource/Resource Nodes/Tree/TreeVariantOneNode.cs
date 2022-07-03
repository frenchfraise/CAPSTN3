using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeVariantOneNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        TreeVariantOneNodePool.pool.Release(this); 
    }
}
