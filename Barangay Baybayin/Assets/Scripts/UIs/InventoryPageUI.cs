using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InventoryPageUI : MonoBehaviour
{
    [SerializeField] ItemCategoryUI prefab;
    [SerializeField] RectTransform container;

    public void GenerateItemCategoryUIs(InventoryPageData p_inventoryPage)
    {
        for (int i = 0; i < p_inventoryPage.itemCategories.Count;)
        {
            ItemCategory currentItemCategory = p_inventoryPage.itemCategories[i];
            ItemCategoryUI newItemCategoryUI = Instantiate(prefab);
            newItemCategoryUI.transform.SetParent(container, false);
            newItemCategoryUI.GenerateItemUIs(currentItemCategory);
            i++;
            if (i >= p_inventoryPage.itemCategories.Count)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
            }

        }
    }
}
