using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemCategoryUI : MonoBehaviour
{
    [SerializeField] ItemUI prefab;
    [SerializeField] RectTransform container;

    public void GenerateItemUIs(ItemCategory p_itemCategory)
    {
        for (int i = 0; i < p_itemCategory.items.Count;)
        {
            ItemData currentItemData = p_itemCategory.items[i];
            ItemUI newItemUI = Instantiate(prefab);
            newItemUI.transform.SetParent(container, false);
            newItemUI.itemData = currentItemData;
            newItemUI.InitializeValues(currentItemData.so_Item.name, "0", currentItemData.so_Item.icon);

            currentItemData.SetItemUI(newItemUI);
            i++;
            if (i >= p_itemCategory.items.Count)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
            }

        }
    }
}
