using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResourceTabUI : MonoBehaviour
{
    [SerializeField] TMP_Text resourceTabUIText;
    [SerializeField] ResourceUI prefab;
    [SerializeField] RectTransform container;

    public void InitializeValues(string p_resourceTabUIName)
    {
        resourceTabUIText.text = p_resourceTabUIName;
    }

    public void GenerateResourceUIs(ResourceCategory p_resourceCategory)
    {
        for (int i = 0; i < p_resourceCategory.resources.Count;)
        {
            Resource currentResource = p_resourceCategory.resources[i];
            ResourceUI newResourceUI = Instantiate(prefab);
            newResourceUI.transform.SetParent(container, false);
            newResourceUI.resourceNameText.text = currentResource.so_Resource.name.ToString();
            newResourceUI.resourceAmountText.text = currentResource.amount.ToString();
            currentResource.amountText = newResourceUI.resourceAmountText;
            i++;
            if (i >= p_resourceCategory.resources.Count)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
            }

        }
    }

}
