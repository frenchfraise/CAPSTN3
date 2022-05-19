using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class RoomInfoUI : MonoBehaviour
{
    public GameObject roomNameGO;
    public TMP_Text roomNameText;
    public GameObject availableResourcesGO;
    public RectTransform availableResourcesContainer;

    public ResourceDropUI resourceDropUIPrefab;

    public void RoomEntered(Passageway p_passageway)
    {
        gameObject.SetActive(true);
        StartCoroutine(Co_Test(p_passageway));
    }
    IEnumerator Co_Test(Passageway p_passageway)
    {
        //Clear resources // object pool this
        for (int si = 0; si < availableResourcesContainer.childCount; si++)
        {
            Destroy(availableResourcesContainer.GetChild(si).gameObject);

        }
        availableResourcesGO.SetActive(false);


        UIManager.instance.transitionUI.gameObject.SetActive(true);
        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);

        //gameObject.SetActive(true);

        roomNameText.text = p_passageway.room.roomName;
        roomNameText.DOFade(1f, 0.75f);
        

        for (int i = 0; i < p_passageway.room.availableResourceNodeDrops.Count; i++)
        {
            ResourceNodeDrop resourceNodeDrop = p_passageway.room.availableResourceNodeDrops[i];
            if (resourceNodeDrop.resourceNode.resourceDrops.Count > 0)
            {
                availableResourcesGO.SetActive(true);
                for (int si = 0; si < resourceNodeDrop.resourceNode.resourceDrops.Count; si++)
                {
                    ResourceDrop resourceDrop = resourceNodeDrop.resourceNode.resourceDrops[si];
                    ResourceDropUI newResourceDropUI = Instantiate(resourceDropUIPrefab);
                    newResourceDropUI.resourceNameText.text = resourceDrop.so_Resource.name;
                    newResourceDropUI.resourceIcon.sprite = resourceDrop.so_Resource.icon;
                    newResourceDropUI.GetComponent<RectTransform>().SetParent(availableResourcesContainer);
                }
            }
           


        }
        CameraManager.instance.onCameraMoved.Invoke(new Vector3(p_passageway.startingCameraPos.x, p_passageway.startingCameraPos.y, Camera.main.transform.position.z));
        yield return new WaitForSeconds(3.75f);
        Sequence t = DOTween.Sequence();
        t.Join(roomNameText.DOFade(0f, 0.5f));
        t.Join(UIManager.instance.transitionUI.DOFade(0, 0.5f));
        availableResourcesGO.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        UIManager.instance.transitionUI.gameObject.SetActive(false);
        PlayerManager.instance.joystick.enabled = true;
    }
}
