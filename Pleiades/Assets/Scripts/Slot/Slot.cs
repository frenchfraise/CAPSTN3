using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Slot : MonoBehaviour
{
    public KeyCode key;
    public bool equipped;
    public ItemSO item;
    public SlotUI slotUI;
    public Action<Slot> OnEquip;

    private void Awake()
    {
        //OnEquip += GetComponent<SkillCaster>().Equip;
        ////OnEquip += skill.Equip;
        OnEquip += slotUI.Equip;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (!equipped)
            {

                OnEquip.Invoke(this);
                Debug.Log("EQUIP");
            }
            else if (equipped)
            {
 


            }
        }

    }
}
