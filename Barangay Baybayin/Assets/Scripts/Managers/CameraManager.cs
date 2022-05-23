using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCameraMoved : UnityEvent<Vector3> { }
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public Camera worldCamera;
    public Camera uiCamera;
    public OnCameraMoved onCameraMoved = new OnCameraMoved();

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        onCameraMoved.AddListener(CameraMoved);
    }

    private void OnDisable()
    {
        onCameraMoved.RemoveListener(CameraMoved);
    }

    public void CameraMoved(Vector3 p_newPosition)
    {
        worldCamera.transform.position = p_newPosition;
    }
}
