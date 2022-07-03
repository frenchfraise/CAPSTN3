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
    public float minChance;// Implement
    public float maxChance;// Implement
    [NonReorderable] public List<SO_Dialogues> dialogue;
    public ParticleSystem particle; //implement this

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
    [NonReorderable] public List<Weather> weathers; //Is for data referencing
    [NonReorderable] public List<Weather> currentWeathers; //Is for actual weather Predictions (this is yours before, it was called Weather in yours)
    public SO_Dialogues currentWeatherDialogue;
    public Weather CurrentWeather => currentWeather;
    [SerializeField] private Weather currentWeather; // Its for the actual weather prediction for the current day
    [HideInInspector] public Weather nextWeather;

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
        TimeManager.onDayChangingEvent.AddListener(RandPredictWeathers);
        onWeatherChangedEvent.AddListener(SwitchWeatherParticleSys);
    }

    private void OnDisable()
    {
        TimeManager.onDayChangingEvent.RemoveListener(RandPredictWeathers);
        onWeatherChangedEvent.RemoveListener(SwitchWeatherParticleSys);
    }

    public Weather GetWeatherUsingName(string p_weatherName)
    {
        for (int i = 0; i < weathers.Count; i++)
        {
            if (weathers[i].name == p_weatherName)
            {
                return weathers[i];
            }
            
        }
        Debug.Log(p_weatherName + " WEATHER NAME DOES NOT EXIST");
        return null;
    }

    public void SwitchWeatherParticleSys(Weather p_currentWeather, Weather p_nextWeather)// NOTE THIS WAS AN ARRAY REVISIT THIS
    {
        
        for (int i = 0; i < weathers.Count; i++)
        {
            if(weathers[i].particle != null)
            {
                weathers[i].particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            
        }
        if (currentWeather.particle != null)
        {
            currentWeather.particle.Play(); //STILL NEED TO CHECK THIS
        }
        //switch (p_currentWeather.name)
        //{
        //    case "Sunny":
        //        cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        break;
        //    case "Cloudy":
        //        cloudParticles.Play();
        //        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        break;
        //    case "Rainy":
        //        cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        rainParticles.Play();
        //        lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        break;
        //    case "Stormy":
        //        cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        //        lightningParticles.Play();
        //        break;
        //}
    }
    // NEW STARTS HERE
    /* For reference:
     * Current Weather = 0
     * Next Weather = 1
     * Next next Weather = 2
     * and so on...*/
    //public void RandPredictWeathers(int num)
    ////public void RandPredictWeathers() // Predicts weathers for 2 days = Current [0] and Next [1] day
    //{
    //    if (randNums[0] == -1)
    //    {

    //        for (int i = 0; i < currentWeathers.Count; i++)
    //        {
    //            randNums[i] = GetRandNum();
    //            bRandomProbs[i] = 0.7f >= randNums[i];
    //            if (bRandomProbs[i])
    //            {
    //                weathers[i] = Weather.Sunny;
    //            }
    //            else weathers[i] = Weather.Rainy;
    //        }
    //    }
    //    else
    //    {
    //        // Set "=" and Stops at the array's end
    //        for (int i = 0; i < currentWeathers.Count - 1; i++)
    //        {
    //            if (i <= currentWeathers.Count - 1)
    //            bRandomProbs[i] = bRandomProbs[i + 1];
    //            if (bRandomProbs[i]) currentWeathers[i] = Weather.Sunny;
    //            else currentWeathers[i] = Weather.Rainy;
    //        }

    //        // Only the end (.Length - 1) randomizes
    //        randNums[currentWeathers.Count - 1] = GetRandNum();
    //        bRandomProbs[currentWeathers.Count - 1] = 0.7f >= randNums[currentWeathers.Count - 1];
    //        if (bRandomProbs[currentWeathers.Count - 1]) weathers[weacurrentWeathersthers.Count - 1] = Weather.Sunny;
    //        else currentWeathers[currentWeathers.Count - 1] = Weather.Rainy;
    //    }

    //    Debug.Log("Current weather: " + currentWeathers[0]);
    //    for (int i = 1; i < currentWeathers.Count; i++)
    //        Debug.Log("Next " + i + " weather's prediction: " + currentWeathers[i]);

    //    onWeatherChangedEvent?.Invoke(currentWeathers[0], currentWeathers);
    //}

    // NEW ENDS HERE




    //OLD REFERENCE STARTS HERE
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
    // OLD REFERENCE ENDS HERE
    

    private int ChooseIndex(int p_maxCount)
    {
        return Random.Range(0, p_maxCount);
    }

    public Weather GetNextWeather(int num)
    {        
        return weathers[num];
    }

    private float GetRandNum()
    {
        float randNum = Random.Range(0, 1f);
        return randNum;
    }
 
}

