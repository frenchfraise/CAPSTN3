using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class CameraMovedEvent : UnityEvent<Vector2,Vector2> { }
public class ShakeCameraEvent : UnityEvent { }
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
    public static ShakeCameraEvent onShakeCameraEvent = new ShakeCameraEvent();
    [SerializeField] Room defaultRoom;

    [SerializeField] float shakePositionDuration = 0.2f;
    [SerializeField] Vector3 shakePositionPower = new Vector3(0.05f, 0.05f);
    [SerializeField] int shakePositionVibrato = 1;
    [SerializeField] float shakePositionRandomRange = 1f;
    [SerializeField] bool shakePositionCanFade = true;

    [SerializeField] float shakeRotationDuration = 0.2f;
    [SerializeField] Vector3 shakeRotationPower = new Vector3(0.05f, 0.05f);
    [SerializeField] int shakeRotationVibrato = 1;
    [SerializeField] float shakeRotationRandomRange = 1f;
    [SerializeField] bool shakeRotationCanFade = true;

    [SerializeField] float zoomInSize = 13f;
    [SerializeField] float zoomInDuration = 1f;

    [SerializeField] float delayTime = 1f;

    [SerializeField] float zoomOutSize = 15f;
    [SerializeField] float zoomOutDuration = 1f;
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
        onShakeCameraEvent.AddListener(ShakeCamera);
        TimeManager.onDayChangingEvent.AddListener(ResetCamera);
        panLimit = Vector2Abs(transform.position - panLimitUpperRightTransform.position);

        ResetCamera();
        StartCoroutine(Co_ZoomCamera());
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

    public void ShakeCamera()
    {
        StartCoroutine(Co_ShakeCamera());
    }
    public IEnumerator Co_ShakeCamera()
    {
        var sequence = DOTween.Sequence()
        .Append(worldCamera.DOShakePosition(shakePositionDuration, shakePositionPower, shakePositionVibrato, shakePositionRandomRange, shakePositionCanFade));
        sequence.Join(worldCamera.DOShakeRotation(shakeRotationDuration, shakeRotationPower, shakeRotationVibrato, shakeRotationRandomRange, shakeRotationCanFade));
        sequence.Play();
        yield return sequence.WaitForCompletion();
    }

    public void ZoomCamera()
    {
        StartCoroutine(Co_ZoomCamera());
    }
    public IEnumerator Co_ZoomCamera()
    {
        var sequence = DOTween.Sequence()
        .Append(worldCamera.DOOrthoSize(zoomInSize, zoomInDuration));

        sequence.Play();
        yield return sequence.WaitForCompletion();
        var sequenceTwo = DOTween.Sequence()
       .Append(worldCamera.DOOrthoSize(zoomOutSize, zoomOutDuration));
        yield return new WaitForSeconds(delayTime);
        sequenceTwo.Play();
        yield return sequenceTwo.WaitForCompletion();
        StartCoroutine(Co_ZoomCamera());
    }
}
