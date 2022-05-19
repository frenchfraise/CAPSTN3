using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HourChanged : UnityEvent<int, int> { };
public class DayChanged : UnityEvent<int> { };

public class TimeManager : MonoBehaviour
{
    public const int hoursInDay = 24, minutesInHour = 60, secondsInMinutes = 60;

    public static TimeManager instance;
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
    protected float realSecondsPerDay;
    [HideInInspector] public float realSecondsPerNight;

    private int dayCount;

    private void Awake()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(Co_NewDay());
        
        // Convert Minutes to Seconds
        realSecondsPerDay = minutesPerDay * secondsInMinutes;
        
        // This is for the day to start at 8 AM
        totalTime = realSecondsPerDay / 3;

        //Test
        //onHourChanged.Invoke(startDayHour, endDayHour);
        //onDayChanged.Invoke(dayCount);
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        currentTime = totalTime % realSecondsPerDay;
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
        string abbreviation = "AM";
        
        if (hour >= 12)
        {
            abbreviation = "PM";
            hour -= 12;
        }

        if (hour == 0) hour = 12;

        return hour.ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00") + " " + abbreviation;
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
        dayCount++;
        ResourceManager.instance.OnRespawn.Invoke();
        StartCoroutine(Co_NewDay());
    }
}
