using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Passageway : MonoBehaviour
{
    public Vector2 startingCameraPos;
    public Room room;
    public Transform playerSpawnTransform;
    public Passageway connectedTo;
    PlayerJoystick plrJoystick;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerJoystick>())
        {
            plrJoystick = collision.gameObject.GetComponent<PlayerJoystick>();
            plrJoystick.enabled = false;
            collision.gameObject.transform.position = connectedTo.playerSpawnTransform.position;
            connectedTo.room.onRoomEntered.Invoke(connectedTo);
            //StartCoroutine(Co_Test());

        }
       

    }
    
    //IEnumerator Co_Test()
    //{

    //    for (int si = 0; si < UIManager.instance.roomInfoUI.availableResourcesContainer.childCount; si++)
    //    {
    //        Destroy(UIManager.instance.roomInfoUI.availableResourcesContainer.GetChild(si).gameObject);

    //    }



    //    UIManager.instance.transitionUI.gameObject.SetActive(true);
    //    UIManager.TransitionFade(1);
    //    yield return new WaitForSeconds(0.5f);
    //    RoomInfoUI test = UIManager.instance.roomInfoUI;
    //    test.gameObject.SetActive(true);

    //    test.roomNameText.text = connectedTo.room.roomName;
    //    test.roomNameText.DOFade(1f, 0.75f);
    //    test.availableResourcesGO.SetActive(true);

    //    for (int i =0; i < connectedTo.room.availableResourceNodeDrops.Count; i++)
    //    {
    //        ResourceNodeDrop resourceNodeDrop = connectedTo.room.availableResourceNodeDrops[i];

    //        for (int si = 0; si < resourceNodeDrop.resourceNode.resourceDrops.Count; si++)
    //        {

    //            ResourceDrop resourceDrop = resourceNodeDrop.resourceNode.resourceDrops[si];
    //            ResourceDropUI newResourceDropUI = Instantiate(UIManager.instance.roomInfoUI.prefab);
    //            newResourceDropUI.resourceNameText.text = resourceDrop.so_Resource.name;
    //            newResourceDropUI.resourceIcon.sprite = resourceDrop.so_Resource.icon;
    //            newResourceDropUI.GetComponent<RectTransform>().SetParent(UIManager.instance.roomInfoUI.availableResourcesContainer);
    //        }


    //    }
    //    Camera.main.transform.position = new Vector3(connectedTo.startingCameraPos.x, connectedTo.startingCameraPos.y, Camera.main.transform.position.z);
    //    yield return new WaitForSeconds(3.75f);
    //    Sequence t = DOTween.Sequence();
    //    t.Join(test.roomNameText.DOFade(0f, 0.5f));
    //    t.Join(UIManager.instance.transitionUI.DOFade(0, 0.5f));
    //    test.availableResourcesGO.SetActive(false);
    //    yield return new WaitForSeconds(0.5f);
    //    test.gameObject.SetActive(false);
    //    UIManager.instance.transitionUI.gameObject.SetActive(false);
    //    plrJoystick.enabled = true;
    //}


}
