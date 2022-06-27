using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] InventoryPageUI prefab;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform contentPanel;
    [SerializeField] List<RectTransform> pages = new List<RectTransform>();
    [SerializeField] int currentPage;
    public void GenerateInventoryPageUIs()
    {
        for (int i = 0; i < InventoryManager.instance.inventoryPages.Count;)
        {
            InventoryPageData currentInventoryPage = InventoryManager.instance.inventoryPages[i];
            InventoryPageUI newInventoryPageUI = Instantiate(prefab);
            newInventoryPageUI.transform.SetParent(container, false);
            newInventoryPageUI.GenerateItemCategoryUIs(currentInventoryPage);

            pages.Add(newInventoryPageUI.GetComponent<RectTransform>());
            i++;
            if (i >= InventoryManager.instance.inventoryPages.Count)
            {
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
                UIManager.instance.StartCoroutine(Co_HotReload());
            }

        }
    }

    public void SnapToNext()
    {
        if (currentPage > 0)
        {
            currentPage--;
            Snap();
        }        
    }
    void Snap()
    {
        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(pages[currentPage].position);
    }
    public void SnapToPrev()
    {
        if (currentPage < pages.Count - 1)
        {
            currentPage++;
            Snap();
        }
    }
    IEnumerator Co_HotReload()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(true);
    }
  
}
