using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float realSecondsPerDay;
    public int dayCount;
    private void Awake()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Co_Test());

    }

    IEnumerator Co_Test()
    {
        Debug.Log("START DAY");
        yield return new WaitForSeconds(realSecondsPerDay);
        dayCount++;
        ResourceManager.instance.OnRespawn.Invoke();
        StartCoroutine(Co_Test());
    }
}
