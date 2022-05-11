using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SkillCaster : MonoBehaviour
{
    public KeyCode key;
    public SkillSO currentSkill;

    public Transform weapon;
    public Animator weaponAnim;
    public Transform aimPoint;
    public float size = 1;
    public Action<SkillCaster> OnSkillCasted;
    public bool canUse;
    public void Awake()
    {
        foreach (Slot currentSlot in gameObject.GetComponents<Slot>())
        {
            if (currentSlot.item.type == ItemType.Weapon)
            {
                currentSlot.OnEquip += GetComponent<SkillCaster>().Equip;
            }


        }

        weaponAnim = weapon.GetComponent<Animator>();
    }

    public void Equip(Slot p_slot)
    {
        if (p_slot.item.type == ItemType.Weapon)
        {
            SkillSO skill = p_slot.item as SkillSO;
            currentSkill = skill;
            OnSkillCasted += skill.Use;
            key = p_slot.key;
            canUse = true;
        }
 

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            aimPoint.position = transform.position + new Vector3(-size*2, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            aimPoint.position = transform.position + new Vector3(size*2, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            aimPoint.position = transform.position + new Vector3(0, size*2, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            aimPoint.position = transform.position + new Vector3(0, -size*2, 0);
        }
        if (canUse)
        {
            if (Input.GetKeyDown(key))
            {
                StartCoroutine(Co_Cooldown(GetComponent<Unit>()));
            }
        }
        

    }

    public IEnumerator Co_Cooldown(Unit p_user)
    {
        canUse = false;
   
        OnSkillCasted.Invoke(this);
        yield return new WaitForSeconds(currentSkill.cooldownRate);
        canUse = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)aimPoint.position, size);
    }
}
