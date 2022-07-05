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
public class WeatherChangedEvent : UnityEvent<List<Weather>, List<Weather>> { };
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
    public Weather CurrentWeather => currentWeathers[0];

    private float[] randNums = new float[4] { -1, -1, -1, -1};
    private bool[] bRandomProbs = new bool[4];

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
        PlayerManager.onUpdateCurrentRoomIDEvent.AddListener(CheckForRoom);        
    }

    private void OnDisable()
    {
        TimeManager.onDayChangingEvent.RemoveListener(RandPredictWeathers);
        onWeatherChangedEvent.RemoveListener(SwitchWeatherParticleSys);
        PlayerManager.onUpdateCurrentRoomIDEvent.RemoveListener(CheckForRoom);
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

    public void SwitchWeatherParticleSys(List<Weather> p_weathers, List<Weather> p_currentWeathers)// NOTE THIS WAS AN ARRAY REVISIT THIS
    {        
        for (int i = 0; i < p_weathers.Count; i++)
        {
            if(p_weathers[i].particle != null)
            {
                p_weathers[i].particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }            
        }
        if (p_currentWeathers[0].particle != null)
        {
            p_currentWeathers[0].particle.Play(); //STILL NEED TO CHECK THIS
        }
    }
    // NEW STARTS HERE
    /* For reference:
     * Current Weather = 0
     * Next Weather = 1
     * Next next Weather = 2
     * and so on...*/
    public void RandPredictWeathers() // Predicts weathers for 2 days = Current [0] and Next [1] day
    {
        if (randNums[0] == -1) // Initialization
        {
            for (int i = 0; i < currentWeathers.Count; i++)
            {
                randNums[i] = GetRandNum();
                bRandomProbs[i] = 0.7f >= randNums[i];
                if (bRandomProbs[i])
                {
                    currentWeathers[i] = weathers[0]; // Sunny
                }
                else currentWeathers[i] = weathers[2]; // Rainy
            }
        }
        else
        {
            // Set at Stops by the array's end
            for (int i = 0; i < currentWeathers.Count - 1; i++)
            {
                if (i <= currentWeathers.Count - 1)
                    bRandomProbs[i] = bRandomProbs[i + 1];
                if (bRandomProbs[i]) currentWeathers[i] = weathers[0]; // Sunny
                else currentWeathers[i] = weathers[2]; // Rainy
            }

            // Only the end (.Length - 1) randomizes
            randNums[currentWeathers.Count - 1] = GetRandNum();
            bRandomProbs[currentWeathers.Count - 1] = 0.7f >= randNums[currentWeathers.Count - 1];
            if (bRandomProbs[currentWeathers.Count - 1]) weathers[currentWeathers.Count - 1] = weathers[0]; // Sunny
            else currentWeathers[currentWeathers.Count - 1] = weathers[2]; // Rainy
        }

        Debug.Log("Current weather: " + currentWeathers[0].name);
        for (int i = 1; i < currentWeathers.Count; i++)
            Debug.Log("Next " + i + " weather's prediction: " + currentWeathers[i].name);

        int chosenIndex = Random.Range(0,currentWeathers[0].dialogue.Count);


        currentWeatherDialogue = currentWeathers[0].dialogue[chosenIndex];
        onWeatherChangedEvent?.Invoke(weathers, currentWeathers);
        PlayerManager.onUpdateCurrentRoomIDEvent.Invoke(8); // TEMPORARY room start
    }
    // NEW ENDS HERE

    private void CheckForRoom(int id)
    {
        if (id == 8)
        {
            if (currentWeathers[0].particle != null)
            {
                currentWeathers[0].particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                currentWeathers[0].particle.Clear();
            }
        }
        else
        {
            if (currentWeathers[0].particle != null)
                currentWeathers[0].particle.Play();
        }
    }
    
    private int ChooseIndex(int p_maxCount)
    {
        return Random.Range(0, p_maxCount);
    }

    public Weather GetNextWeather(int p_num)
    {        
        return currentWeathers[p_num];
    }

    private float GetRandNum()
    {
        float randNum = Random.Range(0, 1f);
        return randNum;
    } 
}

