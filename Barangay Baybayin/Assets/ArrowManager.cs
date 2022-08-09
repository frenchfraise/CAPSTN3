using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.Events;
//public class FirstTimeStoryline : UnityEvent { };
[System.Serializable]
public class MissionPointerData
{
    public bool firstTimeWork = false;
    public bool isSeen = false;
    [NonReorderable][SerializeField] public OnEventDoTransform doTransform;
    public Vector2 currentPosition;
    public Vector2 objectivePosition;
    [SerializeField] public Image _missionPointerImage;
    [SerializeField] public RectTransform _missionDistanceTransform;
    [SerializeField] public RectTransform _missionPointerTransform;
    [SerializeField] public GameObject _missionPointerGameObject;

}
public class ArrowManager : MonoBehaviour
{
    private Camera _cam;
    public Camera uiCam;
    private Transform _camTransform;
    [NonReorderable][SerializeField]
    private List<MissionPointerData> missionPointerData;
    [SerializeField]
    private RectTransform topScreen;
    [SerializeField]
    private RectTransform botScreen;
    [SerializeField]
    private RectTransform rightScreen;
    [SerializeField]
    private RectTransform leftScreen;

    [SerializeField]
    private RectTransform screenSize;
    [SerializeField]
    private float xBorderSize = 125f;
    [SerializeField]
    private float yTopBorderSize = 90f;
    [SerializeField]
    private float yBotBorderSize = 25f;

    bool isInUse = false;

    public Vector3 posOffset;
    public float mult = 1f;
    public Transform upTownToMidTown;
    public Transform midTownToUpTown;
    public Transform midTownToDownTown;
    public Transform midTownToPandayRoom;
    public Transform downTownToMidTown;
    public Transform pandayRoomToMidTown;
    private void Awake()
    {
        _cam = _cam ? _cam : Camera.main;

        _camTransform = _camTransform ? _camTransform : _cam.transform;
        PlayerManager.onUpdateCurrentRoomIDEvent.AddListener(UpdateMissionPointersR);
        PlayerJoystick.onMovedEvent.AddListener(UpdateMissionPointers);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
        StorylineManager.onWorldEventEndedEvent.AddListener(CheckIDMatches);
    }
    private void OnDestroy()
    {
        PlayerManager.onUpdateCurrentRoomIDEvent.RemoveListener(UpdateMissionPointersR);
        PlayerJoystick.onMovedEvent.RemoveListener(UpdateMissionPointers);
        StorylineManager.onWorldEventEndedEvent.RemoveListener(CheckIDMatches);
        UIManager.onGameplayModeChangedEvent.RemoveListener(OnGameplayModeChangedEvent);
    }
    private void OnGameplayModeChangedEvent(bool p_isActive)
    {
  
        // iconHoverEffect.gameObject.SetActive(!p_isActive);
        if (!p_isActive == true)
        {
            if (PlayerManager.instance.currentRoomID == 0 ||
                PlayerManager.instance.currentRoomID == 1 ||
                PlayerManager.instance.currentRoomID == 2 ||
                PlayerManager.instance.currentRoomID == 3)
            {
               
                isInUse = true;
                for (int i = 0; i < missionPointerData.Count; i++)
                {
                    if (missionPointerData[i].firstTimeWork)
                    {
                        ShowPointer(i);
                    }

                }
            }
           
        }
        else if (!p_isActive == false)
        {
            isInUse = false;
            for (int i = 0; i < missionPointerData.Count; i++)
            {
                if (missionPointerData[i].firstTimeWork)
                {
                    HidePointer(i);
                }

            }
        }
    }
    protected void CheckIDMatches(string p_eventID, int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {
        int index = StorylineManager.GetIndexFromID(p_eventID);
        if (p_eventID == "Q-LP" || p_eventID == "Q-KA" || p_eventID == "Q-TA"|| p_eventID == "Q-KL")
        {
            Debug.Log(p_eventID + " STORYLINE INDEX: " + index + " - " + p_actionParameterAID + " - " + p_actionParameterBID);
            if (p_actionParameterAID != -1 && p_actionParameterBID != -1)
            {
                Debug.Log("STORYLINE INDEX: " + index + " - " + p_actionParameterAID + " - " + p_actionParameterBID);
                missionPointerData[index].firstTimeWork = true;

                missionPointerData[index].objectivePosition = (missionPointerData[index].doTransform.actionTransform[p_actionParameterAID].actionPartTransform[p_actionParameterBID].position);
                ShowPointer(index);
                UpdateMissionPointerPosition(index);
                //UpdateMissionPointer(index);
            }
            else
            {
                HidePointer(index);
            }
        }

    }
 
    void ShowPointer(int index)
    {
        missionPointerData[index].isSeen = true;
        missionPointerData[index]._missionPointerGameObject.SetActive(true);
    }
    void HidePointer(int index)
    {
        missionPointerData[index].isSeen = false;
        missionPointerData[index]._missionPointerGameObject.SetActive(false);
    }
    void UpdateMissionPointerInSameRoom(int i)
    {
        bool canSee = true;
        Vector3 posOffset = new Vector3(0,0,0);
        if (missionPointerData[i].isSeen)
        {
            if (PlayerManager.instance.currentRoomID == 0) //Up town
            {
                //posOffset = new Vector3(0f, 225f, 0f);
  
                if (missionPointerData[i].objectivePosition.y < 265f &&
                    missionPointerData[i].objectivePosition.y > 188f) // Up town
                {
           
                    missionPointerData[i].currentPosition = missionPointerData[i].objectivePosition;
                    canSee = false;

                }
                else
                {
    
                    missionPointerData[i].currentPosition = upTownToMidTown.position;
            
                }

            }
            else if (PlayerManager.instance.currentRoomID == 1) // Midtown
            {
                //posOffset = new Vector3(0f, 0f, 0f);
                if (missionPointerData[i].objectivePosition.y < 265f &&
                    missionPointerData[i].objectivePosition.y > 188f) // Up town
                {
                    missionPointerData[i].currentPosition = midTownToUpTown.position;
 
                }
                else if (missionPointerData[i].objectivePosition.y < 35f &&
                    missionPointerData[i].objectivePosition.y > -35f) // Midtown
                {
                    missionPointerData[i].currentPosition = missionPointerData[i].objectivePosition;
                    canSee = false;
                }
                else if (missionPointerData[i].objectivePosition.y < -44f &&
                   missionPointerData[i].objectivePosition.y > -255f) // Down town
                {
                    missionPointerData[i].currentPosition = midTownToDownTown.position;
       
                }
                else if (missionPointerData[i].objectivePosition.y < 145f &&
                      missionPointerData[i].objectivePosition.y > 60f) // Panday House
                {
                    missionPointerData[i].currentPosition = midTownToPandayRoom.position;
                }
             
            }
            else if (PlayerManager.instance.currentRoomID == 2) //Down town
            {
                //posOffset = new Vector3(0f, -150f, 0f);
                if (missionPointerData[i].objectivePosition.y < -44f &&
                    missionPointerData[i].objectivePosition.y > -255f) // Down town
                {
                    missionPointerData[i].currentPosition = missionPointerData[i].objectivePosition;
                    canSee = false;
                }
                else
                {
                    missionPointerData[i].currentPosition = downTownToMidTown.position;
                }
            }
            else if (PlayerManager.instance.currentRoomID == 3) //Panday House
            {
                //posOffset = new Vector3(0f, 100f, 0f);
                if (missionPointerData[i].objectivePosition.y < 145f &&
                        missionPointerData[i].objectivePosition.y > 60f) // Panday House
                {
                    missionPointerData[i].currentPosition = missionPointerData[i].objectivePosition;
                    canSee = false;
                }
                else
                {
                    missionPointerData[i].currentPosition = pandayRoomToMidTown.position;
                }
            }
            UpdateMissionPointer(i, missionPointerData[i].currentPosition, posOffset, canSee);
        }
    }
    void UpdateMissionPointerPosition(int targetIndex = -1)
    {
        if (targetIndex == -1)
        {
            for (int i = 0; i < missionPointerData.Count; i++)
            {
                UpdateMissionPointerInSameRoom(i);

            }
        }
        else
        {
            UpdateMissionPointerInSameRoom(targetIndex);
        }
        
    }
    void UpdateMissionPointersR(int i)
    {
        UpdateMissionPointers();
    }
    void UpdateMissionPointers()

    {
        if (PlayerManager.instance.currentRoomID == 0 ||
                PlayerManager.instance.currentRoomID == 1 ||
                PlayerManager.instance.currentRoomID == 2 ||
                PlayerManager.instance.currentRoomID == 3)
        {
            if (isInUse == false)
            {
                for (int i = 0; i < missionPointerData.Count; i++)
                {
                    if (missionPointerData[i].firstTimeWork)
                    {
                        ShowPointer(i);
                    }

                }
            }
            isInUse = true;
            UpdateMissionPointerPosition();
        }
        else
        {
            if (isInUse == true)
            {
               
                for (int i = 0; i < missionPointerData.Count; i++)
                {
                    if (missionPointerData[i].isSeen)
                    {
                        HidePointer(i);
                    }

                }
            }
            isInUse = false;

        }
        
    }
    void UpdateMissionPointer(int index,Vector3 givenPosition, Vector3 offset, bool canSee)
    {
        Vector3 giv = givenPosition;
        if (givenPosition.x <= PlayerManager.instance.playerTransform.position.x - 31.5)
        {
            giv.x = giv.x * mult;
        }
        Vector3 targetPositionScreenPoint =
     _cam.WorldToScreenPoint(giv - posOffset);

        //bool isOffScreen = targetPositionScreenPoint.x <= leftScreen.anchoredPosition.x ||
        //    targetPositionScreenPoint.x >= rightScreen.anchoredPosition.x ||
        //    targetPositionScreenPoint.y <= botScreen.anchoredPosition.y ||
        //    targetPositionScreenPoint.y >= topScreen.anchoredPosition.y;
        bool isOffScreen = givenPosition.x <= PlayerManager.instance.playerTransform.position.x - 31.5||
                           givenPosition.x >= PlayerManager.instance.playerTransform.position.x + 31.5||
                           givenPosition.y <= PlayerManager.instance.playerTransform.position.y - 22.5||
                           givenPosition.y >= PlayerManager.instance.playerTransform.position.y + 22.5;
        if (isOffScreen) //outside screen
        {
          
            
            if (!missionPointerData[index]._missionPointerImage.enabled)
            {
                missionPointerData[index]._missionPointerImage.enabled = true;
            }



            RotatePointer(index, (givenPosition - posOffset), offset);
            Vector3 cappedTargetScreenPosition =
            targetPositionScreenPoint;

            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, leftScreen.anchoredPosition.x, rightScreen.anchoredPosition.x);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, botScreen.anchoredPosition.y, topScreen.anchoredPosition.y);
            //cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width, Screen.width - 100f);
            //cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height, Screen.height - 100f);

            Vector3 pointerWorldPosition = cappedTargetScreenPosition;
            //Debug.Log(givenPosition + " | " +
            //        cappedTargetScreenPosition + " | " +
            //        leftScreen.position.x + " | " +
            //        rightScreen.position.x + " | " +
            //        botScreen.position.y + " | " +
            //        topScreen.position.y);
            missionPointerData[index]._missionPointerTransform.anchoredPosition = new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, 0f);

            if (!missionPointerData[index]._missionDistanceTransform.gameObject.activeSelf)
            {
                missionPointerData[index]._missionDistanceTransform.gameObject.SetActive(true);
            }
          
        }
        else
        {
            if (canSee)
            {
                if (missionPointerData[index]._missionPointerImage.enabled)
                {
                    missionPointerData[index]._missionPointerImage.enabled = false;
                }
                if (!missionPointerData[index]._missionDistanceTransform.gameObject.activeSelf)
                {
                    missionPointerData[index]._missionDistanceTransform.gameObject.SetActive(true);
                }


                RotatePointer(index, (givenPosition - offset), offset);
                //Vector3 cappedTargetScreenPosition =
                //targetPositionScreenPoint;

                ////cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, leftScreen.anchoredPosition.x, rightScreen.anchoredPosition.x);
                ////cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, botScreen.anchoredPosition.y, topScreen.anchoredPosition.y);
                //cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width, Screen.width - 100f);
                //cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height, Screen.height - 100f);

                //Vector3 pointerWorldPosition = uiCam.ScreenToWorldPoint(cappedTargetScreenPosition);
                ////Debug.Log(givenPosition + " | " +
                ////        cappedTargetScreenPosition + " | " +
                ////        leftScreen.position.x + " | " +
                ////        rightScreen.position.x + " | " +
                ////        botScreen.position.y + " | " +
                ////        topScreen.position.y);
                //missionPointerData[index]._missionPointerTransform.position = pointerWorldPosition;
                //missionPointerData[index]._missionPointerTransform.localPosition = new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, 0f);


            }
            else
            {
                if (missionPointerData[index]._missionPointerImage.enabled)
                {
                    missionPointerData[index]._missionPointerImage.enabled = false;
                }
                if (missionPointerData[index]._missionDistanceTransform.gameObject.activeSelf)
                {
                    missionPointerData[index]._missionDistanceTransform.gameObject.SetActive(false);
                }
            }
          
            
          
            
        }
    }

    private void RotatePointer(int index, Vector2 targetPositionWorld, Vector3 offset)
    {
        Vector2 originPosition = PlayerManager.instance.playerTransform.position - offset;
        Vector2 dir = (targetPositionWorld - originPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        missionPointerData[index]._missionPointerTransform.localEulerAngles = new Vector3(0f, 0f, angle);
        missionPointerData[index]._missionDistanceTransform.localEulerAngles = new Vector3(0f, 0f, -angle);
    }
}
