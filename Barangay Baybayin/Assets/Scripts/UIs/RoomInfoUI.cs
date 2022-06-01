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

  
    public void RoomEntered(Passageway p_passageway)
    {

        string roomName;
        string roomDescription;
        List<ResourceNodeDrop> availableResourceNodeDrops;
        p_passageway.room.GetRoomInfos(out roomName, out roomDescription, out availableResourceNodeDrops);
        Vector2 cameraPosition = new Vector2(p_passageway.cameraDestinationPosition.x,
                                            p_passageway.cameraDestinationPosition.y);
        Vector2 cameraPanLimit = new Vector2(p_passageway.cameraPanLimit.x,
                                            p_passageway.cameraPanLimit.y
                                            );
     

        gameObject.SetActive(true);
        StartCoroutine(Co_RoomInfoUITransition(roomName, roomDescription, availableResourceNodeDrops, cameraPosition, cameraPanLimit));
    }
    IEnumerator Co_RoomInfoUITransition(string p_roomName, string p_roomDescription, List<ResourceNodeDrop> p_availableResourceNodeDrops, Vector2 p_cameraPos, Vector2 p_cameraPanLimit)
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
        CameraManager.instance.onCameraMovedEvent.Invoke(p_cameraPos, p_cameraPanLimit);
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
