using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class RoomInfoUI : MonoBehaviour
{

    public GameObject roomNameGO;
    public TMP_Text roomDescriptionText;
    public TMP_Text roomNameText;
    public GameObject availableResourcesGO;
    public RectTransform availableResourcesContainer;

    public ResourceDropUI resourceDropUIPrefab;

  
    public void RoomEntered(string p_roomName, 
        string p_roomDescription, 
        List<ResourceNodeDrop> p_availableResourceNodeDrops, 
        Vector3 p_cameraPos)
    {
        gameObject.SetActive(true);
        StartCoroutine(Co_RoomInfoUITransition(p_roomName, p_roomDescription, p_availableResourceNodeDrops, p_cameraPos));
    }
    IEnumerator Co_RoomInfoUITransition(string p_roomName, string p_roomDescription, List<ResourceNodeDrop> p_availableResourceNodeDrops, Vector3 p_cameraPos)
    {
        //Clear resources // object pool this
        PlayerManager.instance.joystick.enabled = false;
        for (int si = 0; si < availableResourcesContainer.childCount; si++)
        {
            Destroy(availableResourcesContainer.GetChild(si).gameObject);

        }
        availableResourcesGO.SetActive(false);


        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);



        roomNameText.text = p_roomName;
        roomDescriptionText.text = p_roomDescription;

        Sequence te = DOTween.Sequence();
        te.Join(roomNameText.DOFade(1f, 0.75f));
        te.Join(roomDescriptionText.DOFade(1f, 0.75f));
        
        

        for (int i = 0; i < p_availableResourceNodeDrops.Count; i++)
        {
            ResourceNodeDrop resourceNodeDrop = p_availableResourceNodeDrops[i];
            if (resourceNodeDrop.resourceNode.resourceDrops.Count > 0)
            {
                availableResourcesGO.SetActive(true);
                for (int si = 0; si < resourceNodeDrop.resourceNode.resourceDrops.Count; si++)
                {
                    ResourceDrop resourceDrop = resourceNodeDrop.resourceNode.resourceDrops[si];
                    ResourceDropUI newResourceDropUI = Instantiate(resourceDropUIPrefab);
                    newResourceDropUI.resourceNameText.text = resourceDrop.so_Item.name;
                    newResourceDropUI.resourceIcon.sprite = resourceDrop.so_Item.icon;
                    newResourceDropUI.GetComponent<RectTransform>().SetParent(availableResourcesContainer);
                }
            }
           


        }
        CameraManager.instance.onCameraMoved.Invoke(p_cameraPos);
        yield return new WaitForSeconds(3.75f);
        Sequence t = DOTween.Sequence();
        t.Join(roomNameText.DOFade(0f, 0.5f));
        t.Join(roomDescriptionText.DOFade(0f, 0.5f));

        availableResourcesGO.SetActive(false);
        yield return t.WaitForCompletion();
        gameObject.SetActive(false);
        UIManager.TransitionFade(0, false);

        PlayerManager.instance.joystick.enabled = true;
    }
}
