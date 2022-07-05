using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateCurrentRoomIDEvent : UnityEvent<int> { }


public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }

            return _instance;

        }
    }


    public int currentRoomID = 8;

    public GameObject player;
    public PlayerJoystick playerMovement;
    public Transform playerTransform { get; private set; }
    //[SerializeField] private Stamina stamina;
    [SerializeField] private Bed bed;
    //[SerializeField] public PlayerJoystick joystick;

    public static UpdateCurrentRoomIDEvent onUpdateCurrentRoomIDEvent = new UpdateCurrentRoomIDEvent();

    public static RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();
    private void Awake()
    {
        //if (_instance != null)
        //{
        //    //Destroy(gameObject);
        //}
        //else
        //{
        _instance = this;
            //DontDestroyOnLoad(gameObject);
        //}
        playerTransform = player.transform;
        
    }

    
    private void OnEnable()
    {
        onUpdateCurrentRoomIDEvent.AddListener(UpdateCurrentRoomIDEvent);        
        TimeManager.onDayChangingEvent.AddListener(DayChanging);
    }

    void DayChanging()
    {
        playerTransform.position = bed.spawnTransform.position;
    }
    void UpdateCurrentRoomIDEvent(int p_index)
    {
        currentRoomID = p_index;
    }

  


}
