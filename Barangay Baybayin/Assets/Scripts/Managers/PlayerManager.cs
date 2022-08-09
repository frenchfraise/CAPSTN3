using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

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
    public ToolCaster playerToolCaster;
    public Stamina playerStamina;
    public Transform playerTransform;
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

    public bool canPressPanel = false;
    public Vector2 playerNodePosition;

    public DropTest itemDropPrefab;

    public PauseMenuUI pauseMenuUI;

    public static UpdateCurrentRoomIDEvent onUpdateCurrentRoomIDEvent = new UpdateCurrentRoomIDEvent();

    public static RoomEnteredEvent onRoomEnteredEvent = new RoomEnteredEvent();

    public GameObject trans;

    public GameObject nodeAnnouncer;

    public void RewardResource(List<ResourceDrop> resourceDrops, Vector2 p_vector)
    {
        CameraManager.onShakeCameraEvent.Invoke();
        ToolManager.onResourceNodeFinishedEvent.Invoke();
        int chosenIndex = Random.Range(0, resourceDrops.Count);

        ResourceDrop chosenResourceDrop = resourceDrops[chosenIndex];
        int rewardAmount = Random.Range(chosenResourceDrop.minAmount, chosenResourceDrop.maxAmount);
        for (int i = 0; i < rewardAmount; i++)
        {
            DropTest newI = Instantiate(itemDropPrefab, transform);
            StartCoroutine(newI.test(chosenResourceDrop));
            newI.transform.position = p_vector;
        }
    }
    private void Awake()
    {
        _instance = this;

        playerTransform = player.transform;
        onUpdateCurrentRoomIDEvent.AddListener(UpdateCurrentRoomIDEvent);
        ToolManager.onResourceNodeFinishedEvent.AddListener(playerMovement.OnResourceNodeFinishedEvent);
        TimeManager.onDayChangingEvent.AddListener(DayChanging);
    }

    private void OnDestroy()
    {
        ToolManager.onResourceNodeFinishedEvent.RemoveListener(playerMovement.OnResourceNodeFinishedEvent);
        onUpdateCurrentRoomIDEvent.RemoveListener(UpdateCurrentRoomIDEvent);
        TimeManager.onDayChangingEvent.RemoveListener(DayChanging);
    }

    public void SpawnNewItemFloater(SO_Item p_SOItem, int p_name)
    {
        floaterStackCount += p_name;
        
        if (runningFloaterSpawner != null)
        {
            StopCoroutine(runningFloaterSpawner);
            runningFloaterSpawner = null;
        }
        runningFloaterSpawner = SpawnQueue(p_SOItem, floaterStackCount);
        StartCoroutine(runningFloaterSpawner);       
    }

    public void GoodEndingCheat()
    {
        TimeManager.instance.daysRemaining = 0;
        StorylineManager.instance.amountQuestComplete = 10;
        TimeManager.onDayEndedEvent.Invoke(false, 18);
    }

    public void BadEndingCheat()
    {
        TimeManager.instance.daysRemaining = 0;
        StorylineManager.instance.amountQuestComplete = 0;
        TimeManager.onDayEndedEvent.Invoke(false, 18);
    }
  

    public void GiveCheat()
    {
        InventoryManager.instance.AddAllItems(60);
    }

    public void UpgradeToolCheat()
    {
        if (playerToolCaster.current_Tool.craftLevel < playerToolCaster.current_Tool.so_Tool.maxCraftLevel)
        {
            playerToolCaster.current_Tool.craftLevel++;
          
            for (int i =0; i < ToolManager.instance.tools.Count; i++)
            {
                if (ToolManager.instance.tools[i] == playerToolCaster.current_Tool)
                {
                    ToolManager.onToolCraftLevelUpgradedEvent.Invoke(i);
                }

            }
           
        }
 
    }
    IEnumerator SpawnQueue(SO_Item p_SOItem, int p_amount)
    {
        yield return new WaitForSeconds(0.5f);

        InventoryManager.onAddItemEvent.Invoke(p_SOItem.name, p_amount);
        MaterialFloater newFloater = Instantiate(floaterPrefab);

        offset.x = offset.x * -1;
        isLeft = !isLeft;

        newFloater.InitializeValues(p_SOItem, p_amount.ToString(), playerTransform.position + offset);
        floaterStackCount = 0;
    }
    IEnumerator NodeAnnouncer(string p_TMPAsset, int p_amount)
    {
        nodeAnnouncer.transform.GetChild(0).GetComponent<TMP_Text>().text = p_TMPAsset;
        nodeAnnouncer.transform.GetChild(1).transform.GetComponentInChildren<TMP_Text>().text = $"+{p_amount}";
        nodeAnnouncer.SetActive(true);
        yield return new WaitForSeconds(5f);
        nodeAnnouncer.SetActive(false);
    }

    public void DayChanging()
    {
        playerTransform.position = bed.spawnTransform.position;
        AudioManager.instance.PlayOnRoomEnterString("Town");

    }
    void UpdateCurrentRoomIDEvent(int p_index)
    {
        if (!isFirstTime) currentRoomID = p_index;
        else isFirstTime = false;
    }
}
