using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HourChangedEvent : UnityEvent { };
public class MinuteChangedEvent : UnityEvent { };
public class DayChangedEvent : UnityEvent<int> { };

public class TimeData
{
    int hoursInDay;
    int minutesInHour;
    int secondsInMinutes;

    
}

public class TimeManager : MonoBehaviour
{
    public const int hoursInDay = 24, minutesInHour = 60, secondsInMinutes = 60;

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
    public static HourChangedEvent onHourChangedEvent = new HourChangedEvent();
    public static MinuteChangedEvent onMinuteChangedEvent = new MinuteChangedEvent();
    public static DayChangedEvent onDayChangedEvent = new DayChangedEvent();

    [HideInInspector] public static float sunriseHour = 6;

    private float totalTime = 0;

    // public int intervalNum;
    
    [HideInInspector] public float realSecondsPerNight;

    private int dayCount;
    
    protected float realSecondsPerDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endTime; //not yet used

    public static int minute;
    public static int hour;
    private int numOfHoursAwake = 16;
    public static string abbreviation;

    [SerializeField] private float oneMinToRealSeconds;
    private float timer;

    private Coroutine runningCoroutine;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        minute = 0;
        hour = startHour;

        Formula();

        StartCoroutine(Co_Delay());
        StartCoroutine(Co_DoTimer());
   
    }

    IEnumerator Co_Delay()
    {
        yield return new WaitForSeconds(1f);
        onDayChangedEvent.Invoke(dayCount);
        runningCoroutine = StartCoroutine(Co_NewDay());
    }
    private void OnEnable()
    {
        if (PlayerManager.instance.stamina)
        {
            PlayerManager.instance.stamina.onStaminaDepletedEvent.AddListener(FaintedEndDay);
        }
        dayInfoUI = UIManager.instance.dayInfoUI;
        TimeManager.onDayChangedEvent.AddListener(dayInfoUI.DayEnd);
    }
  
    private void OnDisable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        }
        TimeManager.onDayChangedEvent.RemoveListener(dayInfoUI.DayEnd);
    }
    private void Update()
    {
        totalTime = Time.realtimeSinceStartup;
    }

    IEnumerator Co_DoTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(oneMinToRealSeconds);
            minute++;
            onMinuteChangedEvent?.Invoke();
            if (minute >= 60)
            {
                hour++;
                minute = 0;
                onHourChangedEvent?.Invoke();
                if (hour > hoursInDay)
                {
                    hour = 0;
                }
            }
        }
    }

    public void FaintedEndDay()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        UIManager.instance.dayInfoUI.Faint(dayCount);
        
        onDayChangedEvent.Invoke(dayCount);
    }
    public void EndDay()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        UIManager.instance.dayInfoUI.DayEnd(dayCount);
        onDayChangedEvent.Invoke(dayCount);
    }
    public void NewDay()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        Debug.Log("NEW DAY");
        hour = startHour;
        dayCount++;
        runningCoroutine = StartCoroutine(Co_NewDay());
    }

    IEnumerator Co_NewDay()
    {
        yield return new WaitForSeconds(realSecondsPerDay);
        Debug.Log("NEW DAY co");
        hour = startHour;
        Formula();
        onDayChangedEvent.Invoke(dayCount);
        //TimeManager.instance.OnRespawn.Invoke();
        runningCoroutine=StartCoroutine(Co_NewDay());
    }

    public void Formula()
    {
        realSecondsPerDay = (numOfHoursAwake * minutesInHour) * oneMinToRealSeconds;
    }
}
