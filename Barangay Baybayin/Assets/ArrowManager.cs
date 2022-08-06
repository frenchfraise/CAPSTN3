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
    [SerializeField] public Image _missionPointerImage;
    [SerializeField] public RectTransform _missionDistanceTransform;
    [SerializeField] public RectTransform _missionPointerTransform;
    [SerializeField] public GameObject _missionPointerGameObject;

}
public class ArrowManager : MonoBehaviour
{
    private Camera _cam;
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
        PlayerJoystick.onMovedEvent.AddListener(UpdateMissionPointers);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
        StorylineManager.onWorldEventEndedEvent.AddListener(CheckIDMatches);
    }
    private void OnDestroy()
    {
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

                missionPointerData[index].currentPosition = (missionPointerData[index].doTransform.actionTransform[p_actionParameterAID].actionPartTransform[p_actionParameterBID].position);
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
        if (missionPointerData[i].isSeen)
        {
            if (PlayerManager.instance.currentRoomID == 0) //Up town
            {
                if (missionPointerData[i].currentPosition.y < 265f &&
                    missionPointerData[i].currentPosition.y > 188f) // Up town
                {

                    UpdateMissionPointer(i, missionPointerData[i].currentPosition);
                }
                else
                {
                    UpdateMissionPointer(i, upTownToMidTown.position);
                }
            }
            else if (PlayerManager.instance.currentRoomID == 1) // Midtown
            {
                if (missionPointerData[i].currentPosition.y < 265f &&
                    missionPointerData[i].currentPosition.y > 188f) // Up town
                {

                    UpdateMissionPointer(i, midTownToUpTown.position);
                }
                else if (missionPointerData[i].currentPosition.y < 35f &&
                    missionPointerData[i].currentPosition.y > -35f) // Midtown
                {
                    UpdateMissionPointer(i, missionPointerData[i].currentPosition);
                }
                else if (missionPointerData[i].currentPosition.y < -44f &&
                   missionPointerData[i].currentPosition.y > -255f) // Down town
                {
                    UpdateMissionPointer(i, midTownToDownTown.position);
                }
                else if (missionPointerData[i].currentPosition.y < 145f &&
                      missionPointerData[i].currentPosition.y > 60f) // Panday House
                {
                    UpdateMissionPointer(i, midTownToPandayRoom.position);
                }

            }
            else if (PlayerManager.instance.currentRoomID == 2) //Down town
            {
                if (missionPointerData[i].currentPosition.y < -44f &&
                    missionPointerData[i].currentPosition.y > -255f) // Down town
                {
                    UpdateMissionPointer(i, missionPointerData[i].currentPosition);
                }
                else
                {
                    UpdateMissionPointer(i, downTownToMidTown.position);
                }
            }
            else if (PlayerManager.instance.currentRoomID == 3) //Panday House
            {
                if (missionPointerData[i].currentPosition.y < 145f &&
                        missionPointerData[i].currentPosition.y > 60f) // Panday House
                {
                    UpdateMissionPointer(i, missionPointerData[i].currentPosition);
                }
                else
                {
                    UpdateMissionPointer(i, pandayRoomToMidTown.position);
                }
            }
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
    void UpdateMissionPointer(int index,Vector2 targetPositionWorld)
    {
        float xScaledTopBorderSize = rightScreen.anchoredPosition.x;
        float xScaledBotBorderSize = leftScreen.anchoredPosition.x;
        float yScaledTopBorderSize = topScreen.anchoredPosition.y;
        float yScaledBotBorderSize = botScreen.anchoredPosition.y;

    
        bool isOffScreen = targetPositionWorld.x <= PlayerManager.instance.playerTransform.position.x - 35|| // left
            targetPositionWorld.x >= PlayerManager.instance.playerTransform.position.x + 35 || // right
            targetPositionWorld.y <= PlayerManager.instance.playerTransform.position.y - 15 || // bot
            targetPositionWorld.y >= PlayerManager.instance.playerTransform.position.y + 15; // top

        Vector2 targetPositionScreenPoint =
            _cam.WorldToScreenPoint(targetPositionWorld);

        if (isOffScreen) //outside screen
        {
            RotatePointer(index, targetPositionWorld);
            if (!missionPointerData[index]._missionPointerImage.enabled)
            {
                missionPointerData[index]._missionPointerImage.enabled = true;
            }
            //Debug.Log(targetPositionScreenPoint + 
            //    " OFFSCREEN " +
            //    isOffScreen +
            //    " position " +
            //    PlayerManager.instance.playerTransform.position + 
            //    " bot x " + 
            //    xScaledBotBorderSize + 
            //    " top x " + 
            //    xScaledTopBorderSize +
            //    " bot y " +
            //    yScaledBotBorderSize +
            //    " top y " +
            //    yScaledTopBorderSize);
        
            Vector2 cappedTargetScreenPosition =
            targetPositionScreenPoint;
            Debug.Log(cappedTargetScreenPosition +  " - " +
                xScaledBotBorderSize + " - " +
                xScaledTopBorderSize + " - " +
                yScaledBotBorderSize + " - " +
                yScaledTopBorderSize);

            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, xScaledBotBorderSize, xScaledTopBorderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, yScaledBotBorderSize, yScaledTopBorderSize);
            Debug.Log(cappedTargetScreenPosition);
            Vector2 pointerWorldPosition = (cappedTargetScreenPosition);
            missionPointerData[index]._missionPointerTransform.anchoredPosition = pointerWorldPosition;
            if (!missionPointerData[index]._missionDistanceTransform.gameObject.activeSelf)
            {
                missionPointerData[index]._missionDistanceTransform.gameObject.SetActive(true);
            }

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

    private void RotatePointer(int index, Vector2 targetPositionWorld)
    {
        Vector2 originPosition = _camTransform.position;
        Vector2 dir = (targetPositionWorld - originPosition).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;
        missionPointerData[index]._missionPointerTransform.localEulerAngles = new Vector3(0f, 0f, angle);
        missionPointerData[index]._missionDistanceTransform.localEulerAngles = new Vector3(0f, 0f, -angle);
    }
}
