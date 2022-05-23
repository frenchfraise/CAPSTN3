using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public Transform container;
    public ResourceTabUI prefab;

    private void Start()
    {

        
      
    }
    
    public void BuildInventory()
    {
        for (int i = 0; i < InventoryManager.instance.resourceCategory.Count;)
        {
            ResourceCategory rc = InventoryManager.instance.resourceCategory[i];
            ResourceTabUI newTab = Instantiate(prefab);
            newTab.transform.SetParent(container, false);
            newTab.text.text = rc.name;
            newTab.Test(rc);
            i++;
            if (i >= InventoryManager.instance.resourceCategory.Count)
            {
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
                UIManager.instance.StartCoroutine(test());
            }

        }
    }

    IEnumerator test()
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
