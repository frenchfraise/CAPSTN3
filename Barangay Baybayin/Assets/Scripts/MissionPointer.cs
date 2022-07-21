using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionPointer : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    private Transform _camTransform;
   
    [SerializeField] private Image _missionPointerImage;
    [SerializeField] private RectTransform _missionDistanceTransform;
    [SerializeField] private RectTransform _missionPointerTransform;

    [SerializeField] private Sprite _arrowSprite;
    [SerializeField] private Sprite _missionIconSprite;

    [SerializeField] private Vector2 _targetPosition;




    private void Awake()
    {
        _cam = _cam ? _cam : Camera.main;
        _camTransform = _camTransform ? _camTransform : _cam.transform;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Vector3 targetPosition)
    {
        gameObject.SetActive(true);
        _targetPosition = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {

        float borderSize = 25f;
        Vector2 targetPositionScreenPoint = _cam.WorldToScreenPoint(_targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen) //outside screen
        {
            
            RotatePointer();
            _missionPointerImage.enabled = true;

            _missionDistanceTransform.gameObject.SetActive(true);
       
            Vector2 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize)
            {
                cappedTargetScreenPosition.x = borderSize;
            }
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize)
            {
                cappedTargetScreenPosition.x = Screen.width - borderSize;
            }
            if (cappedTargetScreenPosition.y <= borderSize)
            {
                cappedTargetScreenPosition.y = borderSize;
            }
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize)
            {
                cappedTargetScreenPosition.y = Screen.height - borderSize;
            }
            Vector2 pointerWorldPosition = (cappedTargetScreenPosition);
            _missionPointerTransform.position = pointerWorldPosition;
 

        }
        else
        {
       
        
            _missionPointerImage.enabled = false;
            _missionDistanceTransform.gameObject.SetActive(false);


            Vector2 pointerWorldPosition = (targetPositionScreenPoint);
     
  

            
        }
        _missionDistanceTransform.GetComponent<Text>().text = Mathf.RoundToInt(Vector2.Distance(_camTransform.position, 
            _targetPosition)).ToString() + "m";


    }

    private void RotatePointer()
    {
        Vector2 originPosition = _camTransform.position;
        Vector2 dir = (_targetPosition - originPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        _missionPointerTransform.localEulerAngles = new Vector3(0f, 0f, angle);
        _missionDistanceTransform.localEulerAngles = new Vector3(0f, 0f, -angle);

    }

    
}
