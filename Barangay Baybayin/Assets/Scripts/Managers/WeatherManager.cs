using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Weather
{
    public string name;
    public Sprite sprite;
    public string audioName;
    [NonReorderable] public List<SO_Dialogues> dialogue;
    [SerializeField] ParticleSystem particle; //implement this

}
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
    [NonReorderable] public List<Weather> weathers;
    public SO_Dialogues currentWeatherDialogue;
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
        TimeManager.onDayChangingEvent.AddListener(RandPredictWeathers);
        onWeatherChangedEvent.AddListener(SwitchWeatherParticleSys);
    }

    private void OnDisable()
    {
        TimeManager.onDayChangingEvent.RemoveListener(RandPredictWeathers);
        onWeatherChangedEvent.RemoveListener(SwitchWeatherParticleSys);
    }

    public void SwitchWeatherParticleSys(Weather p_currentWeather, Weather p_nextWeather)
    { 
        switch (p_currentWeather.name)
        {
            case "Sunny":
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case "Cloudy":
                cloudParticles.Play();
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case "Rainy":
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Play();
                lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                break;
            case "Stormy":
                cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                lightningParticles.Play();
                break;
        }
    }
    public void RandPredictWeathers() // Predicts weathers for 2 days = Current [0] and Next [1] day
    {
        Debug.Log("WEATHER RANDOMIZING");
        if (randNums[0] == -1) // DAY 1 initialization
        {
            //Debug.Log("Randomized!");
            randNums[0] = GetRandNum();
            randNums[1] = GetRandNum();
            bRandomProbs[0] = 0.7f >= randNums[0];
            bRandomProbs[1] = 0.7f >= randNums[1];

            if (bRandomProbs[1]) nextWeather = weathers[0];
            else nextWeather = weathers[2];         
        }
        else // DAY++ caching to randNum[0] and randomizing randNum[1]
        {            
            //Debug.Log("bRandomProbs[1] passed to bRandomProbs[1]!");
            bRandomProbs[0] = bRandomProbs[1];

            //Debug.Log("Randomized! DAY++");
            randNums[1] = GetRandNum();
            bRandomProbs[1] = 0.7f >= randNums[1];

            if (bRandomProbs[1]) nextWeather = weathers[0];
            else nextWeather = weathers[2];
        }

        Debug.Log("Next weather's prediction: " + nextWeather);

        // Debug.Log("bRandomProbs[0]: " + bRandomProbs[0]);        
        if (bRandomProbs[0]) currentWeather = weathers[0];
        else currentWeather = weathers[2];
        Debug.Log("Current weather: " + currentWeather);

        currentWeatherDialogue = currentWeather.dialogue[ChooseIndex(currentWeather.dialogue.Count)];

        onWeatherChangedEvent?.Invoke(currentWeather, nextWeather);
    }

    private int ChooseIndex(int p_maxCount)
    {
        return Random.Range(0, p_maxCount);
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

