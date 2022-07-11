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
    [SerializeField] public Camera worldCamera;
    [SerializeField] public Camera uiCamera;
    [HideInInspector] public CameraMovement cameraMovement;
    [SerializeField] private Transform panLimitUpperRightTransform;

    [HideInInspector]
    public Vector2 panLimit;
    public static CameraMovedEvent onCameraMovedEvent = new CameraMovedEvent();
    [SerializeField] Room defaultRoom;

    private void Awake()
    {
       
        _instance = this;
        //If condition is true then do expression 1, else do expression 2
        worldCamera = worldCamera ? worldCamera : GameObject.Find("World Camera").GetComponent<Camera>();
        worldCamera = worldCamera ? worldCamera : GameObject.Find("UI Camera").GetComponent<Camera>();
        cameraMovement = worldCamera.GetComponent<CameraMovement>();
    }

    private void OnEnable()
    {
    
        TimeManager.onDayChangingEvent.AddListener(ResetCamera);
        panLimit = Vector2Abs(transform.position - panLimitUpperRightTransform.position);

        ResetCamera();
        
    }

    private void OnDisable()
    {
        TimeManager.onDayChangingEvent.RemoveListener(ResetCamera);
    }
    Vector2 Vector2Abs(Vector2 p_vector2)
    {
        Vector2 answer = new Vector2(Mathf.Abs(p_vector2.x), Mathf.Abs(p_vector2.y));
        return answer;
    }
    public void ResetCamera()
    {
        //Debug.Log("Invoked: " + defaultRoom.transform.position + ", " + panLimit);
        onCameraMovedEvent.Invoke(defaultRoom.transform.position, panLimit);
    }

    
}
