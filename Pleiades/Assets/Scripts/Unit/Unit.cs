using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public ElementalType currentElementalType;
    //public ElementalTypeSO currentElementalType;
    //Id delete
    public float curHp;
    public float maxHp;
    public int atkDmg;
    public int damage;


    public float Attack()
    {
        return atkDmg;
    }
}
