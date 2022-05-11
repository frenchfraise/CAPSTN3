using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Skill/Melee")]
public class MeleeSkillSO : SkillSO
{
    public override void Equip(Slot p_slot)
    {
        // p_slot.OnUse += Attack;
    }
    public override void Use(SkillCaster p_unit)//damageTransform, playerAnim, weapon anim
    {
        //p_unit.StartCoroutine(AttackCo(p_unit));
        Collider2D[] collider = Physics2D.OverlapCircleAll((Vector2)p_unit.aimPoint.position, 1f);
        foreach (Collider2D hit in collider)
        {
            if (hit.gameObject != p_unit.gameObject)
            {
                if (hit != null)
                {
                    Debug.Log("HIT " + hit.gameObject.name);
                    Health targetHealth = hit.gameObject.GetComponent<Health>();

                    if (targetHealth)
                    {
                        Unit targetUnit = hit.gameObject.GetComponent<Unit>();
                        float effectiveDamage = damage;

                        ElementalType targetElement = hit.gameObject.GetComponent<ElementalType>();
                        if (targetElement)
                        {
                            effectiveDamage *= ElementalSystem.GetEffectivity(elementalType, targetElement.elementalType);
                        }
                        
                        targetHealth.OnDamaged.Invoke(p_unit.gameObject, effectiveDamage);

                    }
                }
            }

        }

        Animator animator = p_unit.GetComponent<Animator>();
        //animator.SetBool("Attacking", true);
        animator.SetTrigger("Attack");

    }


    public IEnumerator AttackCo(Unit p_user)
    {
        Debug.Log("Coroutine Attack start");

        yield return new WaitForSeconds(.1f);
    }
}
