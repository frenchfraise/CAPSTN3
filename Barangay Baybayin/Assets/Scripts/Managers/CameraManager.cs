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
    public static CameraMovedEvent onCameraMovedEvent = new CameraMovedEvent();
    [SerializeField] Room defaultRoom;

    private void Awake()
    {
        //if (_instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
            _instance = this;
            DontDestroyOnLoad(gameObject);
        //}
        cameraMovement = worldCamera.GetComponent<CameraMovement>();
    }

    private void OnEnable()
    {
    
        TimeManager.onDayChangingEvent.AddListener(ResetCamera);
    }

    private void OnDisable()
    {
       // onCameraMovedEvent.RemoveListener(CameraMoved);
    }

    public void ResetCamera()
    {
        onCameraMovedEvent.Invoke(defaultRoom.cameraDestinationPosition, defaultRoom.cameraPanLimit);
    }

    public void MoveCamera(Vector2 p_newPosition,Vector2 p_panLimit)
    {
        Debug.Log(p_newPosition + " - " + p_panLimit);
        onCameraMovedEvent.Invoke(p_newPosition, p_panLimit);
    }
}
