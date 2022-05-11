using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public enum ElementalAttribute
//{
//    water,
//    fire,
//    electricity
//}
public static class ElementalSystem
{
    public static float GetEffectivity(ElementalTypeSO p_casterElement, ElementalTypeSO p_targetElement)
    {
        if (p_targetElement.name.ToLower() == p_casterElement.name.ToLower()) //if it is the same type, deal no damage
        {
            // deal no damage;
            return 0f;
        }
        else // if not same type
        {
            for (int i = 0; i < p_targetElement.weakAgainst.Count; i++)
            {
                if (p_targetElement.weakAgainst[i].name.ToLower() == p_casterElement.name.ToLower()) // If the attacker's element is the weakness, deal double damage
                {
                    //Deal double damage
                    //break;
                    return 2f;
                }
             
            }
        }
        return 1f; // If it isnt same type, and it isnt the weakness, then just deal normal damage
     
    }
}
