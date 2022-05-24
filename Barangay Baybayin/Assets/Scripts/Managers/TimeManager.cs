using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HourChanged : UnityEvent<int, int> { };
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
    public DayChanged onDayChanged = new DayChanged();

    //make all these private (i just made them public so just in case events cant be used, you can use these)
    // public float realSecondsPerHour;
    // public float startDayHour;
    // public float endDayHour;
    [HideInInspector] public float sunriseHour = 6;

    private float totalTime = 0;
    private float currentTime = 0;

    public float minutesPerDay;
    public int intervalNum;
    
    [HideInInspector] public float realSecondsPerNight;

    private int dayCount;
    
    protected float realSecondsPerDay;
    [SerializeField] private float startTime; //not yet used
    [SerializeField] private float endTime; //not yet used

    private Coroutine runningCoroutine;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
        
        // Convert Minutes to Seconds
        realSecondsPerDay = minutesPerDay * secondsInMinutes;
        
        // This is for the day to start at 8 AM
        totalTime = realSecondsPerDay /3;

        StartCoroutine(delay());
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
        TimeManager.instance.onDayChanged.AddListener(UIManager.instance.dayInfoUI.test);
    }
  
    private void OnDisable()
    {
        if (PlayerManager.instance)
        {
            PlayerManager.instance.stamina.onStaminaDepleted.RemoveListener(FaintedEndDay);

        }
        TimeManager.instance.onDayChanged.RemoveListener(UIManager.instance.dayInfoUI.test);
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
        currentTime = totalTime % realSecondsPerDay;
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
        UIManager.instance.dayInfoUI.test(dayCount);
        onDayChanged.Invoke(dayCount);

    }
    public void NewDay()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        Debug.Log("NEW DAY");
        dayCount++;
        runningCoroutine = StartCoroutine(Co_NewDay());
    }

    public float GetHour()
    {
        return currentTime * hoursInDay / realSecondsPerDay;
    }

    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / realSecondsPerDay) % minutesInHour;
    }

    public string Clock12Hour()
    {
        // Debug.Log(GetHour());
        int hour = Mathf.FloorToInt(GetHour());
        int mins = Mathf.FloorToInt(GetMinutes());
        string abbreviation = "AM";
        
        if (hour >= 12)
        {
            abbreviation = "PM";
            hour -= 12;
        }

        if (hour == 0) hour = 12;

        mins = mins / intervalNum % intervalNum;

        if (mins == 1)
        {
            mins = intervalNum;
            //Debug.Log(mins);
            return hour.ToString("00") + ":" + mins.ToString() + " " + abbreviation;
        }
        return hour.ToString("00") + ":" + mins.ToString() + "0" + " " + abbreviation;
    }

    public void OnHourChanged()
    {
        
    }

    public void OnDayChanged()
    {

    }

    IEnumerator Co_NewDay()
    {
        yield return new WaitForSeconds(realSecondsPerDay);
        Debug.Log("NEW DAY co");
        totalTime = realSecondsPerDay / 3;
        currentTime = 0;
        onDayChanged.Invoke(dayCount);
        //TimeManager.instance.OnRespawn.Invoke();
        runningCoroutine=StartCoroutine(Co_NewDay());
    }
}
