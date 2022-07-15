using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class RoomInfoUI : MonoBehaviour
{

    public GameObject roomNameGO;
    public TMP_Text roomDescriptionText;
    public TMP_Text roomNameText;
    public GameObject availableResourcesGO;
    public RectTransform availableResourcesContainer;
    public GridLayoutGroup gridLayout;
    public ResourceDropUI resourceDropUIPrefab;
    public List<ResourceDrop> currentRoomsResourceDrops;
  

    private void Start()
    {
        PlayerManager.onRoomEnteredEvent.AddListener(RoomEntered);
        gameObject.SetActive(false);
    }
    private void Destroy()
    {
        PlayerManager.onRoomEnteredEvent.RemoveListener(RoomEntered);
    }
    public void RoomEntered(Passageway p_passageway)
    {
        //PlayerManager.instance.joystick.enabled = false;
        PlayerJoystick.onUpdateJoystickEnabledEvent.Invoke(false);
        TimeManager.onPauseGameTime.Invoke(false);
        //UIManager.onGameplayModeChangedEvent.Invoke(true);
        string roomName;
        string roomDescription;
        List<ResourceNodeDrop> availableResourceNodeDrops;
        p_passageway.room.GetRoomInfos(out roomName, out roomDescription, out availableResourceNodeDrops);
        Vector2 cameraPosition = new Vector2(p_passageway.cameraDestinationPosition.x,
                                            p_passageway.cameraDestinationPosition.y);
        Vector2 cameraPanLimit = new Vector2(p_passageway.cameraPanLimit.x,
                                            p_passageway.cameraPanLimit.y
                                            );

        //PlayerManager.instance.currentRoomID = p_passageway.room.currentRoomID;
        gameObject.SetActive(true);        
        StartCoroutine(Co_RoomInfoUITransition(roomName, roomDescription, availableResourceNodeDrops, cameraPosition, cameraPanLimit));
    }
    IEnumerator Co_RoomInfoUITransition(string p_roomName, string p_roomDescription, List<ResourceNodeDrop> p_availableResourceNodeDrops, Vector2 p_cameraPos, Vector2 p_cameraPanLimit)
    {
        //Clear resources // object pool this
        for (int si = 0; si < availableResourcesContainer.childCount; si++)
        {
            //Debug.Log("DELETE");
            Destroy(availableResourcesContainer.GetChild(si).gameObject);

        }
        currentRoomsResourceDrops.Clear();
        availableResourcesGO.SetActive(false);
   

        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
     


        roomNameText.text = p_roomName;
        roomDescriptionText.text = p_roomDescription;

        Sequence te = DOTween.Sequence();
        te.Join(roomNameText.DOFade(1f, 0.75f));
        te.Join(roomDescriptionText.DOFade(1f, 0.75f));
        te.Play();


        for (int i = 0; i < p_availableResourceNodeDrops.Count; i++)
        {
            ResourceNodeDrop resourceNodeDrop = p_availableResourceNodeDrops[i];
            if (resourceNodeDrop.resourceNode.resourceDrops.Count > 0)
            {
                availableResourcesGO.SetActive(true);
                for (int si = 0; si < resourceNodeDrop.resourceNode.resourceDrops.Count; si++)
                {
                    ResourceDrop resourceDrop = resourceNodeDrop.resourceNode.resourceDrops[si];
                    //Check if already in list
                    if (currentRoomsResourceDrops.Count > 0)
                    {
                        for (int x = 0; x < currentRoomsResourceDrops.Count;)
                        {
                            if (resourceDrop.so_Item == currentRoomsResourceDrops[x].so_Item)
                            {
                                break;
                            }
                            x++;
                            if (x >= currentRoomsResourceDrops.Count)
                            {
                                currentRoomsResourceDrops.Add(resourceDrop);
                                ResourceDropUI newResourceDropUI = Instantiate(resourceDropUIPrefab);
                                // newResourceDropUI.resourceNameText.text = resourceDrop.so_Item.name;
                                newResourceDropUI.resourceIcon.sprite = resourceDrop.so_Item.icon;
                                RectTransform newResourceDropUITransform = newResourceDropUI.GetComponent<RectTransform>();
                                newResourceDropUITransform.SetParent(availableResourcesContainer, true);
                                newResourceDropUITransform.localScale = new Vector3(1, 1, 1);
                                break;
                            }

                        }
                    }
                    else
                    {
                        currentRoomsResourceDrops.Add(resourceDrop);
                        ResourceDropUI newResourceDropUI = Instantiate(resourceDropUIPrefab);
                        newResourceDropUI.resourceNameText.text = resourceDrop.so_Item.name;
                        newResourceDropUI.resourceIcon.sprite = resourceDrop.so_Item.icon;
                        RectTransform newResourceDropUITransform = newResourceDropUI.GetComponent<RectTransform>();
                        newResourceDropUITransform.SetParent(availableResourcesContainer,true);
                        newResourceDropUITransform.localScale = new Vector3(1, 1, 1);

                    }
                    
                }
            }
     
        }
        CameraManager.onCameraMovedEvent.Invoke(p_cameraPos, p_cameraPanLimit);
        yield return new WaitForSeconds(3.75f);
        Sequence t = DOTween.Sequence();
        t.Join(roomNameText.DOFade(0f, 0.5f));
        t.Join(roomDescriptionText.DOFade(0f, 0.5f));
        t.Play();
        availableResourcesGO.SetActive(false);
        Debug.Log("ROOM INFO PERFORMING");
        yield return t.WaitForCompletion();
        Debug.Log("ROOM INFO ENDING");
        gameObject.SetActive(false);
        UIManager.TransitionFade(0, false);
        //UIManager.onGameplayModeChangedEvent.Invoke(false);
        PlayerJoystick.onUpdateJoystickEnabledEvent.Invoke(true);
        TimeManager.onPauseGameTime.Invoke(true);
        //PlayerManager.instance.joystick.enabled = true;
        //PlayerJoystick.onUpdateJoystickEnabledEvent.Invoke(true);
    }
}
