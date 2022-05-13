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
    
        Camera.main.transform.position = new Vector3(connectedTo.startingCameraPos.x, connectedTo.startingCameraPos.y, Camera.main.transform.position.z);
        collision.gameObject.transform.position = connectedTo.playerSpawnTransform.position;

    }



}
