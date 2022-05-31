using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HourChanged : UnityEvent { };
public class MinuteChanged : UnityEvent { };
public class DayChanged : UnityEvent<int> { };

public class TimeManager : MonoBehaviour
{
    public const int hoursInDay = 24, minutesInHour = 60, secondsInMinutes = 60;

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
    public HourChanged onHourChanged = new HourChanged();
    public MinuteChanged onMinuteChanged = new MinuteChanged();
    public DayChanged onDayChanged = new DayChanged();

    [HideInInspector] public float sunriseHour = 6;

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

        StartCoroutine(delay());
        StartCoroutine(DoTimer());
        //Test
        //onHourChanged.Invoke(startDayHour, endDayHour);
        //onDayChanged.Invoke(dayCount);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        onDayChanged.Invoke(dayCount);
        runningCoroutine = StartCoroutine(Co_NewDay());
    }
    private void OnEnable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepleted.AddListener(FaintedEndDay);
        }
        TimeManager.instance.onDayChanged.AddListener(UIManager.instance.dayInfoUI.DayEnd);
    }
  
    private void OnDisable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepleted.RemoveListener(FaintedEndDay);
        }
        TimeManager.instance.onDayChanged.RemoveListener(UIManager.instance.dayInfoUI.DayEnd);
    }
    private void Update()
    {
        totalTime = Time.realtimeSinceStartup;
    }

    IEnumerator DoTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(oneMinToRealSeconds);
            minute++;
            onMinuteChanged?.Invoke();
            if (minute >= 60)
            {
                hour++;
                minute = 0;
                onHourChanged?.Invoke();
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
        
        onDayChanged.Invoke(dayCount);
    }
    public void EndDay()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        UIManager.instance.dayInfoUI.DayEnd(dayCount);
        onDayChanged.Invoke(dayCount);
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
        onDayChanged.Invoke(dayCount);
        //TimeManager.instance.OnRespawn.Invoke();
        runningCoroutine=StartCoroutine(Co_NewDay());
    }

    public void Formula()
    {
        realSecondsPerDay = (numOfHoursAwake * minutesInHour) * oneMinToRealSeconds;
    }
}
