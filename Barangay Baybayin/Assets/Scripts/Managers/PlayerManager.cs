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
    public Stamina playerStamina;
    public Transform playerTransform { get; private set; }
    public Room playerRoom;
    [SerializeField] private Bed bed;
    [SerializeField] public Passageway startRoomPassageway;
    [SerializeField] private MaterialFloater floaterPrefab;
    [SerializeField] private int floaterStackCount;
    [SerializeField] private IEnumerator runningFloaterSpawner;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float delay;
    private bool isLeft;
    bool isFirstTime = true;
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
        onUpdateCurrentRoomIDEvent.AddListener(UpdateCurrentRoomIDEvent);
        TimeManager.onDayChangingEvent.AddListener(DayChanging);
    }

    private void OnDestroy()
    {
        onUpdateCurrentRoomIDEvent.RemoveListener(UpdateCurrentRoomIDEvent);
        TimeManager.onDayChangingEvent.RemoveListener(DayChanging);
    }

    public void SpawnNewItemFloater(SO_Item p_SOItem, int p_name)
    {
        floaterStackCount++;
        if (runningFloaterSpawner == null)
        {
            if (runningFloaterSpawner != null)
            {
                StopCoroutine(runningFloaterSpawner);
                runningFloaterSpawner = null;
            }
            runningFloaterSpawner = SpawnQueue(p_SOItem, p_name);
            StartCoroutine(runningFloaterSpawner);
        }
       
    }

    IEnumerator SpawnQueue(SO_Item p_SOItem, int p_amount)
    {
        InventoryManager.onAddItemEvent.Invoke(p_SOItem.name, p_amount);
        MaterialFloater newFloater = Instantiate(floaterPrefab);

        //if (isLeft) // go right
        //{

        //    offset = offset * -1;
        //}
        //else // go left
        //{

        //}
        int amt;
        offset.x = offset.x * -1;
        isLeft = !isLeft;
        if (floaterStackCount > 2)
        {
            floaterStackCount -= 2;
            amt = 2;
        }
        else
        {
            amt = 1;
            floaterStackCount--;
        }
        newFloater.InitializeValues(p_SOItem, amt.ToString(), playerTransform.position + offset);

        yield return new WaitForSeconds(delay);
        if (floaterStackCount > 0)
        {
            runningFloaterSpawner = SpawnQueue(p_SOItem, p_amount);
            if (runningFloaterSpawner != null)
            {
              
                runningFloaterSpawner = null;
            }
            runningFloaterSpawner = SpawnQueue(p_SOItem, p_amount);
            StartCoroutine(runningFloaterSpawner);
        }
        else
        {
            runningFloaterSpawner = null;
        }
 
        
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    public void DayChanging()
    {
        playerTransform.position = bed.spawnTransform.position;
        AudioManager.instance.PlayOnRoomEnterString("Town");
        //onRoomEnteredEvent.Invoke(startRoomPassageway);
        //Debug.Log("ID: " + startRoomPassageway.room.currentRoomID);
        //onUpdateCurrentRoomIDEvent.Invoke(startRoomPassageway.room.currentRoomID);
    }
    void UpdateCurrentRoomIDEvent(int p_index)
    {
        if (!isFirstTime) currentRoomID = p_index;
        else isFirstTime = false;
    }
}
