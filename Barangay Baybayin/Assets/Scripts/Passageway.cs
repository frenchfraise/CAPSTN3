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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.gameObject.transform.position = connectedTo.playerSpawnTransform.position;
        StartCoroutine(Co_Test());

    }

    IEnumerator Co_Test()
    {
        UIManager.instance.transitionUI.gameObject.SetActive(true);
        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        RoomInfoUI test = UIManager.instance.roomInfoUI;
        test.gameObject.SetActive(true);
        //test.roomNameGO
        test.roomNameText.text = connectedTo.room.roomName;
        test.roomNameText.DOFade(1f, 0.75f);
        test.availableResourcesGO.SetActive(true);
        //test.availableResourcesContainer. add the reosurce to this

        Camera.main.transform.position = new Vector3(connectedTo.startingCameraPos.x, connectedTo.startingCameraPos.y, Camera.main.transform.position.z);
        yield return new WaitForSeconds(3.75f);
        Sequence t = DOTween.Sequence();
        t.Join(test.roomNameText.DOFade(0f, 0.5f));
        t.Join(UIManager.instance.transitionUI.DOFade(0, 0.5f));
        test.availableResourcesGO.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        test.gameObject.SetActive(false);
        UIManager.instance.transitionUI.gameObject.SetActive(false);

    }


}
