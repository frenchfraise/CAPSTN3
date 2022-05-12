using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireDeathEvent : ObjectRequirement
{
    public override void OnEnable()
    {
        base.OnEnable();
        GetComponent<Health>().OnDeath += RequirementCleared;
    }
}
