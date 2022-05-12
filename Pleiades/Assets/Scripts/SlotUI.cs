using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUI : MonoBehaviour
{

    public GameObject slotLock;

    public GameObject slotGameObject;
    [HideInInspector] public RectTransform slotTransform;
    public void Equip(Slot p_gemSlot)
    {
        if (p_gemSlot.slotUI == this)
        {
         
            p_gemSlot.equipped = true;
            slotLock.SetActive(false);
        }
        else
        {
            p_gemSlot.equipped = false;
            slotLock.SetActive(true);
        }
    }
}
