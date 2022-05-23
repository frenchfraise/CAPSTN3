using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResourceTabUI : MonoBehaviour
{
    public TMP_Text text;
    public ResourceUI resourceUIPrefab;
    public RectTransform container;

    public void Test(ResourceCategory rc)
    {
        foreach (Resource r in rc.resources)
        {
            ResourceUI newR = Instantiate(resourceUIPrefab);
            newR.transform.SetParent(container,false);
            newR.resourceNameText.text = r.so_Resource.name.ToString();
            newR.resourceAmountText.text = r.amount.ToString();
            r.text = newR.resourceAmountText;
        }
        for (int i = 0; i < rc.resources.Count;)
        {
            Resource r = rc.resources[i];
            ResourceUI newR = Instantiate(resourceUIPrefab);
            newR.transform.SetParent(container, false);
            newR.resourceNameText.text = r.so_Resource.name.ToString();
            newR.resourceAmountText.text = r.amount.ToString();
            r.text = newR.resourceAmountText;
            i++;
            if (i >= rc.resources.Count)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
            }

        }
    }

    private void OnDisable()
    {

    }
}
