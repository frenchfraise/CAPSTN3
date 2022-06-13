using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeChangedEvent : UnityEvent<int, int, int> { };
public class DayChangedEvent : UnityEvent<int> { };

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

    [HideInInspector] public static float sunriseHour = 6;

    private float totalTime = 0;
    
    [HideInInspector] public float realSecondsPerNight;

    private int dayCount;
    
    protected float realSecondsPerDay;
    [SerializeField] private int startHour;
    [SerializeField] private int endTime; //not yet used

    public static int minute;
    private int minuteByTwos;
    public static int minuteByTens;
    public int hour;
    private int numOfHoursAwake = 8;
    public static string abbreviation;

    [SerializeField] private float oneMinToRealSeconds;

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
        //dayInfoUI = dayInfoUI?UIManager.instance.dayInfoUI:FindObjectOfType<DayInfoUI>();
        onDayChangedEvent.AddListener(dayInfoUI.DayEnd);
    }
  
    private void OnDisable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepletedEvent.RemoveListener(FaintedEndDay);
        }
        onDayChangedEvent.RemoveListener(dayInfoUI.DayEnd);
    }
    private void Update()
    {
        // Real Time played
        // totalTime += Time.realtimeSinceStartup;
    }

    IEnumerator Co_DoTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(oneMinToRealSeconds);
            minute++;
            minuteByTwos = minute;
            minuteByTwos *= 2;            
            if (minuteByTwos % 10 == 0)
            {
                minuteByTens = minuteByTwos;
            }
            if (minute >= minutesInHour)
            {
                hour++;
                minute = 0;
                minuteByTens = 0;
                if (hour > hoursInDay)
                {
                    hour = 0;
                }
            }
            onTimeChangedEvent?.Invoke(hour, minute, minuteByTens);
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
