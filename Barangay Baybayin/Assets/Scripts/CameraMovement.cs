using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //[HideInInspector] 
    public Vector2 offset;
    //[HideInInspector] 
    public Vector2 panLimit;
    private void Update()
    {
        Vector3 pos =  PlayerManager.instance.stamina.gameObject.transform.position;

        pos.x = Mathf.Clamp(pos.x, offset.x + -panLimit.x, offset.x + panLimit.x);
        pos.y = Mathf.Clamp(pos.y, offset.y + -panLimit.y, offset.y + panLimit.y);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
