using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovedEvent : UnityEvent<Vector2,Vector2> { }
public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CameraManager>();
            }

            return _instance;
        }
    }
    public Camera worldCamera;
    [HideInInspector] public CameraMovement cameraMovement;
    public Camera uiCamera;
    public CameraMovedEvent onCameraMovedEvent = new CameraMovedEvent();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        cameraMovement = worldCamera.GetComponent<CameraMovement>();
    }

    private void OnEnable()
    {
        onCameraMovedEvent.AddListener(CameraMoved);
    }

    private void OnDisable()
    {
        onCameraMovedEvent.RemoveListener(CameraMoved);
    }

    public void CameraMoved(Vector2 p_newPosition,Vector2 p_panLimit)
    {
        cameraMovement.offset = new Vector3(p_newPosition.x, p_newPosition.y);
        //,worldCamera.transform.position.z);
        cameraMovement.panLimit = p_panLimit;
    }
}
