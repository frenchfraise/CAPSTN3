using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "ScriptableObjects/Items/Skill")]
public class SkillSO : ItemSO
{
    public ElementalTypeSO elementalType;
    public float damage;
    public float fireRate;
    public float cooldownRate;
    public virtual void Equip(Slot p_registeringFunction)
    {
       // p_registeringFunction += Use;
    }

    public virtual void Use(SkillCaster p_unit)
    {

    }
}
