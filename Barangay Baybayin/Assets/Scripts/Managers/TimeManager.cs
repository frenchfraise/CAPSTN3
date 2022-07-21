using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeChangedEvent : UnityEvent<int, int, int> { };
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
    private int hour;
    public static string abbreviation;

    [SerializeField] private float oneMinToRealSeconds;

    public bool DoTimer;
    public IEnumerator runningTime;
    public bool tutorialOn = true;

    private void Awake()
    {
        _instance = this;
        Stamina.onStaminaDepletedEvent.AddListener(FaintedEndDay);
        Bed.onBedInteractedEvent.AddListener(EndDay);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
        onPauseGameTime.AddListener(SetPauseGame);
        onTimeChangedEvent.AddListener(OnTimeCheck);
    }

    private void OnDestroy()
    {
        Stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        Bed.onBedInteractedEvent.RemoveListener(EndDay);
        UIManager.onGameplayModeChangedEvent.RemoveListener(OnGameplayModeChangedEvent);
        onPauseGameTime.RemoveListener(SetPauseGame);
        onTimeChangedEvent.RemoveListener(OnTimeCheck);
    }

    void Start()
    {
        minute = 0;
        hour = startHour;
        tutorialOn = true;
        //onDayEndedEvent.Invoke(false,dayCount);
        onHourChanged.Invoke(hour); //TEMPORARY
        
        DoTimer = true;
        //NewDay();
        //StartCoroutine(Co_DoTimer());
    }

    private void OnEnable()
    {


        //dayInfoUI = UIManager.instance.dayInfoUI;
        //dayInfoUI = dayInfoUI?UIManager.instance.dayInfoUI:FindObjectOfType<DayInfoUI>();

        //onDayChangeEndedEvent.AddListener(NewDay);

        
    }

    private void OnDisable()
    {
        
        //Stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        //Bed.onBedInteractedEvent.RemoveListener(EndDay);

        //onPauseGameTime.RemoveListener(SetPauseGame);
        //onTimeChangedEvent.AddListener(OnTimeCheck);

    }
    private void OnGameplayModeChangedEvent(bool p_bool)
    {
      //  Debug.Log("RAAAAAAAAAAAA");
        SetPauseGame(!p_bool);

    }
    IEnumerator ForceTest()
    {
        yield return new WaitForSeconds(2f);
        onHourChanged.Invoke(hour); //TEMPORARY
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
                    hour++;
                    if (hour > hoursInDay) hour = 0;
                    onHourChanged.Invoke(hour); //TEMPORARY
                    minute = 0;
                    minuteByTens = 0;
                }
                onTimeChangedEvent?.Invoke(hour, minute, minuteByTens);

            }
        }
        
        
    }

    private void OnTimeCheck(int p_hour, int p_minute, int p_minuteByTens)
    {
        if (p_hour >= endHour)
        {
            hour = startHour;
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
        TimeManager.instance.hour = TimeManager.instance.startHour;
       
        minute = 0;
        minuteByTwos = 0;
        minuteByTens = 0;
        onTimeChangedEvent?.Invoke(hour, minute, minuteByTens);
        TimeManager.instance.dayCount++;
        onHourChanged.Invoke(hour);
        TimeManager.onDayChangeEndedEvent.Invoke();
        StorylineManager.onWorldEventEndedEvent.Invoke("W",0,dayCount);
    }    

    private void SetPauseGame(bool p_bool)
    {
       // Debug.Log("time " + p_bool);
        if (!tutorialOn)
        {
            DoTimer = p_bool;
            if (p_bool) StartCoroutine(Co_DoTimer());
        }    
   
    }
    
    /* Krabby Patty Formuler
     * realSecondsPerDay = (numOfHoursAwake * minutesInHour) * oneMinToRealSeconds;*/
}
