using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeChangedEvent : UnityEvent<int, int, int> { };
public class DayChangedEvent : UnityEvent<int> { };

public class HourChangedEvent : UnityEvent { };

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
    private DayInfoUI dayInfoUI;
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
    public static DayChangedEvent onDayChangedEvent = new DayChangedEvent();
    public static HourChangedEvent onHourChanged = new HourChangedEvent();
    [HideInInspector] public static float sunriseHour = 6;

    private float totalTime = 0; // This is for realtime game played (hours played and such)

    private int dayCount;

    protected float realSecondsPerDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;

    public static int minute;
    private int minuteByTwos;
    public static int minuteByTens;
    private int hour;
    public static string abbreviation;

    [SerializeField] private float oneMinToRealSeconds;

    private bool DoTimer;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        minute = 0;
        hour = startHour;
        
        onDayChangedEvent.Invoke(dayCount);
        onHourChanged.Invoke(); //TEMPORARY
        
        DoTimer = true;
        StartCoroutine(Co_DoTimer());
    }

    private void OnEnable()
    {
        if (PlayerManager.instance.stamina)
        {
            PlayerManager.instance.stamina.onStaminaDepletedEvent.AddListener(FaintedEndDay);
        }
        dayInfoUI = UIManager.instance.dayInfoUI;
        //dayInfoUI = dayInfoUI?UIManager.instance.dayInfoUI:FindObjectOfType<DayInfoUI>();
        onDayChangedEvent.AddListener(dayInfoUI.DayEnd);
        UIManager.instance.onPauseGameTime.AddListener(SetPauseGame);
        onTimeChangedEvent.AddListener(OnTimeCheck);
    }

    private void OnDisable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        }
        onDayChangedEvent.RemoveListener(dayInfoUI.DayEnd);
        if (UIManager.instance) UIManager.instance.onPauseGameTime.RemoveListener(SetPauseGame);
        onTimeChangedEvent.AddListener(OnTimeCheck);
    }

    IEnumerator ForceTest()
    {
        yield return new WaitForSeconds(2f);
        onHourChanged.Invoke(); //TEMPORARY
    }

    IEnumerator Co_DoTimer()
    {
        while (DoTimer)
        {
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
                onHourChanged.Invoke(); //TEMPORARY
                minute = 0;
                minuteByTens = 0;
            }
            onTimeChangedEvent?.Invoke(hour, minute, minuteByTens);
        }
    }

    private void OnTimeCheck(int p_hour, int p_minute, int p_minuteByTens)
    {
        if (p_hour >= endHour)
        {
            hour = startHour;
            onDayChangedEvent.Invoke(dayCount);
        }
    }

    public void FaintedEndDay()
    {
        UIManager.instance.dayInfoUI.Faint(dayCount);
        onDayChangedEvent.Invoke(dayCount);
    }
    public void EndDay()
    {
        UIManager.instance.dayInfoUI.DayEnd(dayCount);
        onDayChangedEvent.Invoke(dayCount);
    }
    public void NewDay()
    {
        Debug.Log("NEW DAY");
        hour = startHour;
        dayCount++;
    }    

    private void SetPauseGame(bool p_bool)
    {
        DoTimer = p_bool;
        if (p_bool) StartCoroutine(Co_DoTimer());
    }
    
    /* Krabby Patty Formuler
     * realSecondsPerDay = (numOfHoursAwake * minutesInHour) * oneMinToRealSeconds;*/
}
