using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VetiverLeafVariantOneNode : ResourceNode
{
    public override void DeinitializeValues()
    {
        base.DeinitializeValues();
        VetiverLeafVariantOneNodePool.pool.Release(this);
    }
}
