using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HourChanged : UnityEvent<int, int> { };
public class DayChanged : UnityEvent<int> { };

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public HourChanged onHourChanged = new HourChanged();
    public DayChanged onDayChanged = new DayChanged();

    //make all these private (i just made them public so just in case events cant be used, you can use these)
    public float realSecondsPerHour;
    public int startDayHour;
    public int endDayHour;

    public float realSecondsPerDay;
    public int dayCount;
    private void Awake()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Co_NewDay());

        //Test
        //onHourChanged.Invoke(startDayHour, endDayHour);
        //onDayChanged.Invoke(dayCount);
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
