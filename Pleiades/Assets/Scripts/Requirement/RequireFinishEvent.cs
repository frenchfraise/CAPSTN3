using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireFinishEvent : ObjectRequirement
{

    public override void OnEnable()
    {
        base.OnEnable();
        GetComponent<EnemySpawner>().OnFinishedSpawning += RequirementCleared;
    }
}
