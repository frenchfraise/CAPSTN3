using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] ResourceTabUI prefab;

    public void GenerateResourceTabUIs()
    {
        for (int i = 0; i < InventoryManager.instance.resourceCategory.Count;)
        {
            ResourceCategory currentResourceCategory = InventoryManager.instance.resourceCategory[i];
            ResourceTabUI newTabUI = Instantiate(prefab);
            newTabUI.transform.SetParent(container, false);
            newTabUI.InitializeValues(currentResourceCategory.name);
            newTabUI.GenerateResourceUIs(currentResourceCategory);
            i++;
            if (i >= InventoryManager.instance.resourceCategory.Count)
            {
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
                UIManager.instance.StartCoroutine(Co_HotReload());
            }

        }
    }

    IEnumerator Co_HotReload()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
    
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
