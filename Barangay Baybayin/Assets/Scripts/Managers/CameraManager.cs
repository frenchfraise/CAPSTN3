using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCameraMoved : UnityEvent<Vector3> { }
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
    public Camera uiCamera;
    public OnCameraMoved onCameraMoved = new OnCameraMoved();

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
