using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeatherChangedEvent : UnityEvent<Weather, Weather[]> { };
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
    
    public Weather CurrentWeather => Weathers[0];
    private Weather[] Weathers = new Weather[3];

    private float[] randNums = new float[3] { -1, -1, -1};
    private bool[] bRandomProbs = new bool[3];

    [SerializeField] ParticleSystem cloudParticles;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem lightningParticles;
    
    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        TimeManager.onDayChangedEvent.AddListener(RandPredictWeathers);
        onWeatherChangedEvent.AddListener(SwitchWeatherParticleSys);
    }

    private void OnDisable()
    {
        TimeManager.onDayChangedEvent.RemoveListener(RandPredictWeathers);
        onWeatherChangedEvent.RemoveListener(SwitchWeatherParticleSys);
    }

    public void SwitchWeatherParticleSys(Weather p_currentWeather, Weather[] p_nextWeather)
    { 
        switch (p_currentWeather)
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

    /* For reference:
     * Current Weather = 0
     * Next Weather = 1
     * Next next Weather = 2
     * and so on...*/
    public void RandPredictWeathers(int num)
    {
        if (randNums[0] == -1)
        {
            for (int i = 0; i < Weathers.Length; i++)
            {
                randNums[i] = GetRandNum();
                bRandomProbs[i] = 0.7f >= randNums[i];
                if (bRandomProbs[i]) Weathers[i] = Weather.Sunny;
                else Weathers[i] = Weather.Rainy;
            }
        }
        else
        {
            // Set "=" and Stops at the array's end
            for (int i = 0; i < Weathers.Length - 1; i++)
            {
                if (i <= Weathers.Length - 1)
                bRandomProbs[i] = bRandomProbs[i + 1];
                if (bRandomProbs[i]) Weathers[i] = Weather.Sunny;
                else Weathers[i] = Weather.Rainy;
            }

            // Only the end (.Length - 1) randomizes
            randNums[Weathers.Length - 1] = GetRandNum();
            bRandomProbs[Weathers.Length - 1] = 0.7f >= randNums[Weathers.Length - 1];
            if (bRandomProbs[Weathers.Length - 1]) Weathers[Weathers.Length - 1] = Weather.Sunny;
            else Weathers[Weathers.Length - 1] = Weather.Rainy;
        }

        Debug.Log("Current weather: " + Weathers[0]);
        for (int i = 1; i < Weathers.Length; i++)
            Debug.Log("Next " + i + " weather's prediction: " + Weathers[i]);

        onWeatherChangedEvent?.Invoke(Weathers[0], Weathers);
    }

    public Weather GetNextWeather(int num)
    {        
        return Weathers[num];
    }

    private float GetRandNum()
    {
        float randNum = Random.Range(0, 1f);
        return randNum;
    }
 
}

public enum Weather
{
    Sunny,
    Cloudy,
    Rainy,
    Stormy
}
