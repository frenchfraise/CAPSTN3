using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeatherChangedEvent : UnityEvent<Weather> { };
public class WeatherManager : MonoBehaviour
{
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

    public static WeatherChangedEvent onWeatherChangedEvent = new WeatherChangedEvent();
    public List<SO_Dialogues> sunnyDialogues;
    public List<SO_Dialogues> cloudyDialogues;
    public List<SO_Dialogues> rainyDialogues;
    public List<SO_Dialogues> stormyDialogues;
    [SerializeField] private Weather currentWeather = Weather.Stormy;
    public Weather CurrentWeather => currentWeather;

    [SerializeField] ParticleSystem cloudParticles;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem lightningParticles;
    
    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        TimeManager.onDayChangedEvent.AddListener(GetRandWeather);
        onWeatherChangedEvent.AddListener(CheckWeather);
    }

    private void OnDisable()
    {
        TimeManager.onDayChangedEvent.RemoveListener(GetRandWeather);
        onWeatherChangedEvent.RemoveListener(CheckWeather);
    }

    public void CheckWeather(Weather p_weather)
    { 
        switch (currentWeather)
        {
            case Weather.Sunny:
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Cloudy:
                cloudParticles.Play();
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Rainy:
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Play();
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case Weather.Stormy:
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Play();
                break;
        }
    }
    public void GetRandWeather(int num)
    {
        float randNum = Random.Range(0f, 1f);        
        bool bRandomProb = 0.7f >= randNum;
        if (bRandomProb) currentWeather = Weather.Sunny;
        else currentWeather = Weather.Rainy;
        onWeatherChangedEvent?.Invoke(currentWeather);
    }
}

public enum Weather
{
    Sunny,
    Cloudy,
    Rainy,
    Stormy
}
