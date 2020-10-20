using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float curHp;
    public float maxHp;
    public float atkDmg;
    public float damage;


    public float Attack()
    {
        return atkDmg;
    }
}
