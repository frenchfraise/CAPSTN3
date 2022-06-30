using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeatherChangedEvent : UnityEvent<Weather, Weather> { };
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
    
    public Weather CurrentWeather => currentWeather;
    [SerializeField] private Weather currentWeather;
    [HideInInspector] public Weather nextWeather;

    private float[] randNums = new float[2] { -1, -1,};
    private bool[] bRandomProbs = new bool[2];

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

    public void SwitchWeatherParticleSys(Weather p_currentWeather, Weather p_nextWeather)
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
    public void RandPredictWeathers(int num) // Predicts weathers for 2 days = Current [0] and Next [1] day
    {
        if (randNums[0] == -1) // DAY 1 initialization
        {
            //Debug.Log("Randomized!");
            randNums[0] = GetRandNum();
            randNums[1] = GetRandNum();
            bRandomProbs[0] = 0.7f >= randNums[0];
            bRandomProbs[1] = 0.7f >= randNums[1];

            if (bRandomProbs[1]) nextWeather = Weather.Sunny;
            else nextWeather = Weather.Rainy;         
        }
        else // DAY++ caching to randNum[0] and randomizing randNum[1]
        {            
            //Debug.Log("bRandomProbs[1] passed to bRandomProbs[1]!");
            bRandomProbs[0] = bRandomProbs[1];

            //Debug.Log("Randomized! DAY++");
            randNums[1] = GetRandNum();
            bRandomProbs[1] = 0.7f >= randNums[1];

            if (bRandomProbs[1]) nextWeather = Weather.Sunny;
            else nextWeather = Weather.Rainy;
        }

        Debug.Log("Next weather's prediction: " + nextWeather);

        // Debug.Log("bRandomProbs[0]: " + bRandomProbs[0]);        
        if (bRandomProbs[0]) currentWeather = Weather.Sunny;
        else currentWeather = Weather.Rainy;
        Debug.Log("Current weather: " + currentWeather);
        
        onWeatherChangedEvent?.Invoke(currentWeather, nextWeather);
    }

    public Weather GetNextWeather()
    {        
        return nextWeather;
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
