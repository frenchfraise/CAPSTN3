using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
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

    }

    IEnumerator Co_NewDay()
    {
        yield return new WaitForSeconds(realSecondsPerDay);
        dayCount++;
        ResourceManager.instance.OnRespawn.Invoke();
        StartCoroutine(Co_NewDay());
    }
}
