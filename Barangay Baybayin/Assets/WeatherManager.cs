using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherType
{
    storm,
    rain,
    sunny,
}

public class WeatherManager : MonoBehaviour
{
    public WeatherType currentWeatherType;
    private static WeatherManager _instance;
    public static WeatherManager instance


    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<WeatherManager>();
            }

            return _instance;
        }
    }
  
    private void Awake()
    {


        _instance = this;
        DontDestroyOnLoad(gameObject);


    }
}
