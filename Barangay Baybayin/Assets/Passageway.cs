using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Passageway : MonoBehaviour
{
    public Vector2 startingCameraPos; 
    public Transform playerSpawnTransform;
    public Passageway connectedTo;
    private void OnTriggerExit2D(Collider2D collision)
    {

        collision.gameObject.transform.position = connectedTo.playerSpawnTransform.position;
        StartCoroutine(Co_Test());

    }

    IEnumerator Co_Test()
    {
        UIManager.TransitionFade(1);
        yield return new WaitForSeconds(0.5f);
        Camera.main.transform.position = new Vector3(connectedTo.startingCameraPos.x, connectedTo.startingCameraPos.y, Camera.main.transform.position.z);
       
        UIManager.TransitionFade(0);
    }


}
