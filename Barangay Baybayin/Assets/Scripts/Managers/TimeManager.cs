using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class TimeChangedEvent : UnityEvent<int, int, int,int> { };
public class DayEndedEvent : UnityEvent<bool,int> { };

public class DayChangingEvent : UnityEvent { };

public class DayChangeEndedEvent : UnityEvent { };
public class HourChangedEvent : UnityEvent<int> { };
public class PauseGameTimeUI : UnityEvent<bool> { }
public class TimeData
{
    int hoursInDay;
    int minutesInHour;
    int secondsInMinutes;
}

public class TimeManager : MonoBehaviour
{
    public const int hoursInDay = 24, minutesInHour = 30, secondsInMinutes = 60;

    private static TimeManager _instance;
    public static TimeManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TimeManager>();
            }

            return _instance;
        }
    }

    public static TimeChangedEvent onTimeChangedEvent = new TimeChangedEvent();
    public static DayEndedEvent onDayEndedEvent = new DayEndedEvent();
    public static DayChangingEvent onDayChangingEvent = new DayChangingEvent();
    public static DayChangeEndedEvent onDayChangeEndedEvent = new DayChangeEndedEvent();
    public static HourChangedEvent onHourChanged = new HourChangedEvent();
    public static PauseGameTimeUI onPauseGameTime = new PauseGameTimeUI();
    [HideInInspector] public static float sunriseHour = 6;

    private float totalTime = 0; // This is for realtime game played (hours played and such)

    [SerializeField] public int dayCount;

    protected float realSecondsPerDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;

    public static int minute;
    private int minuteByTwos;
    public static int minuteByTens;
    private int hour24;
    private int hour12;
    public string abbreviation { get; private set; }

    public int daysRemaining =16;
    public int maxDays = 16;

    [SerializeField] private float oneMinToRealSeconds;

    public bool DoTimer;
    public IEnumerator runningTime;
    public IEnumerator coroutineTime;
    public bool tutorialOn = true;

    private void Awake()
    {
        _instance = this;
        Stamina.onStaminaDepletedEvent.AddListener(FaintedEndDay);
        Bed.onBedInteractedEvent.AddListener(EndDay);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
        StorylineManager.onWorldEventEndedEvent.AddListener(worldEndEvent);
        onPauseGameTime.AddListener(SetPauseGame);
        onTimeChangedEvent.AddListener(OnTimeCheck);
    }
   
    private void OnDestroy()
    {
        Stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        Bed.onBedInteractedEvent.RemoveListener(EndDay);
        UIManager.onGameplayModeChangedEvent.RemoveListener(OnGameplayModeChangedEvent);
        StorylineManager.onWorldEventEndedEvent.RemoveListener(worldEndEvent);
        onPauseGameTime.RemoveListener(SetPauseGame);
        onTimeChangedEvent.RemoveListener(OnTimeCheck);
    }

    void Start()
    {
        minute = 0;
        hour24 = startHour;
        hour12 = startHour;
        abbreviation = "AM";
        tutorialOn = true;
        //onDayEndedEvent.Invoke(false,dayCount);
        onHourChanged.Invoke(hour24); //TEMPORARY
        
        DoTimer = true;
        //NewDay();            
    }

    void worldEndEvent(string p_id, int p_intone, int p_intto)
    {
        if (p_id == "GOODENDING" || p_id == "BADENDING")
        {

            SceneManager.LoadScene(0);
         
        }
        else if (p_id == "TE-0")
        {
            TimeManager.onDayEndedEvent.Invoke(false, 2);
        }
        if (p_id == "TE-0" || p_id == "TE-1" || p_id == "TE-2" )
        {
            StartCoroutine(test());

        }
    }

    IEnumerator test()
    {

        yield return new WaitForSeconds(5f);
        //PlayerManager.instance.canPressPanel = true;
        UIManager.instance.characterDialogueUI.SetEndTransitionEnabledEvent(true);
        UIManager.instance.characterDialogueUI.SetStartTransitionEnabledEvent(true);
    }
  
    private void OnGameplayModeChangedEvent(bool p_bool)
    {
     
     

    }
    IEnumerator ForceTest()
    {
        yield return new WaitForSeconds(2f);
        onHourChanged.Invoke(hour24); //TEMPORARY
    }

    IEnumerator Co_DoTimer()
    {
        if (!tutorialOn)
        {
            while (DoTimer)
            {
                //Debug.Log("test");
                yield return new WaitForSeconds(oneMinToRealSeconds);
                minute++;
                minuteByTwos = minute * 2;
                if (minuteByTwos % 10 == 0)
                {
                    minuteByTens = minuteByTwos;
                }
                if (minute >= minutesInHour)
                {
                    hour24++;
                    hour12++;
                    if (hour24 > hoursInDay) hour24 = 0;
                    if (hour12 > 11)
                    {
                        abbreviation = "PM";
                        if (hour12 > 12) hour12 = 1;
                    }
                    onHourChanged.Invoke(hour24); //TEMPORARY
                    minute = 0;
                    minuteByTens = 0;
                }
                onTimeChangedEvent?.Invoke(hour24, hour12, minute, minuteByTens);
            }
        }
    }

    private void OnTimeCheck(int p_hour24, int p_hour12, int p_minute, int p_minuteByTens)
    {
        if (p_hour24 >= endHour)
        {
            hour24 = startHour;
            hour12 = startHour;
            abbreviation = "AM";
            onDayEndedEvent.Invoke(false,dayCount);
        }
    }

    public void FaintedEndDay()
    {       
        // UIManager.instance.dayInfoUI.Faint(dayCount);
        onDayEndedEvent.Invoke(true,dayCount);
    }
    public void EndDay()
    {
        // UIManager.instance.dayInfoUI.DayEnd(dayCount);        
        onDayEndedEvent.Invoke(false,dayCount);
    }
    public void NewDay()
    {
        Debug.Log("NEW DAY");
        hour24 = startHour;
        hour12 = startHour;
        minute = 0;
        minuteByTwos = 0;
        minuteByTens = 0;
        onTimeChangedEvent?.Invoke(hour24, hour12, minute, minuteByTens);
        TimeManager.instance.dayCount++;
        onHourChanged.Invoke(hour24);
        TimeManager.onDayChangeEndedEvent.Invoke();
        if (daysRemaining > 0)
        {
            daysRemaining--;
            
            StorylineManager.onWorldEventEndedEvent.Invoke("W", 0, dayCount);
        }
        else
        {
   
            if (StorylineManager.instance.amountQuestComplete >= 8)
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("GOODENDING", StorylineManager.instance.goodso_dialogue);
            }
            else
            {
                CharacterDialogueUI.onCharacterSpokenToEvent.Invoke("BADENDING", StorylineManager.instance.badso_dialogue);
            }
        }
    }

    private void SetPauseGame(bool p_bool)
    {
       // Debug.Log("time " + p_bool);
        if (!tutorialOn)
        {
            if (PlayerManager.instance.currentRoomID != PlayerManager.instance.playerRoom.currentRoomID)
            {
               // Debug.Log("DDDDDDDDD " + p_bool);
                DoTimer = p_bool;
                if (coroutineTime != null)
                {
                    StopCoroutine(coroutineTime);
                    coroutineTime = null;
                }
                coroutineTime = Co_DoTimer();
                if (p_bool)
                {
                    StartCoroutine(coroutineTime);
                }
                //if (p_bool) StartCoroutine(Co_DoTimer());
            }
            else
            {
                DoTimer = false;
                if (coroutineTime != null)
                {
                    StopCoroutine(coroutineTime);
                    coroutineTime = null;
                }
            }
        }
    }
    
    /* Krabby Patty Formuler
     * realSecondsPerDay = (numOfHoursAwake * minutesInHour) * oneMinToRealSeconds;*/
}
