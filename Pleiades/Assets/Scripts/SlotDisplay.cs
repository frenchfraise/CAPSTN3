using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDisplay : MonoBehaviour
{
    public List<SlotUI> slotUIs = new List<SlotUI>();
    private void Awake()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].slotTransform = slotUIs[i].slotGameObject.GetComponent<RectTransform>();
            // gemSlots[i].OnEquip += Equip;
        }
    }
}
