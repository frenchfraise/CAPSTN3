using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
 
    public Vector2 panLimit;
    private void Update()
    {
        Vector3 pos = transform.position;
        pos = PlayerManager.instance.stamina.gameObject.transform.position;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
