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
    [SerializeField] public CharacterEmotionType emotion;
   // [NonReorderable] public List<SO_Dialogues> dialogue;
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
    public string currentText;
    public CharacterEmotionType currentEmotion;
    [NonReorderable] public List<string> currentWeatherFillersDialogue;
    [NonReorderable] public List<string> predictedWeatherFillersDialogue;
    public Weather CurrentWeather => currentWeathers[0];

    private float[] randNums = new float[4] { -1, -1, -1, -1 };
    private bool[] bRandomProbs = new bool[4];

    [SerializeField] ParticleSystem cloudParticles;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem lightningParticles;
    
    private void Awake()
    {
        _instance = this;
        PlayerManager.onUpdateCurrentRoomIDEvent.AddListener(CheckForRoom);
        TimeManager.onDayChangingEvent.AddListener(RandPredictWeathers);
        onWeatherChangedEvent.AddListener(SwitchWeatherParticleSys);
    }
  
    private void OnDestroy()
    {
        PlayerManager.onUpdateCurrentRoomIDEvent.RemoveListener(CheckForRoom);
        TimeManager.onDayChangingEvent.RemoveListener(RandPredictWeathers);
        onWeatherChangedEvent.RemoveListener(SwitchWeatherParticleSys);
    }
    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
   
    }

    public Weather GetWeatherName(string p_weatherName)
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
            p_currentWeathers[0].particle.Play();
        }
    }
    /* For reference:
     * Current Weather = 0
     * Next Weather = 1
     * Next next Weather = 2
     * and so on...*/
    public void RandPredictWeathers() // Predicts weathers for 2 days = Current [0] and Next [1] day
    {        
        //if (isStormy)
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

        //Debug.Log("Current weather: " + currentWeathers[0].name);
        //for (int i = 1; i < currentWeathers.Count; i++)
        //    Debug.Log("Next " + i + " weather's prediction: " + currentWeathers[i].name);      

        int chosenCurrentWeatherFillerDialogueIndex = Random.Range(0, currentWeatherFillersDialogue.Count);
        int chosenFirstPredictedWeatherFillerDialogueIndex = Random.Range(0, predictedWeatherFillersDialogue.Count);
        int chosenSecondPredictedWeatherFillerDialogueIndex = Random.Range(0, predictedWeatherFillersDialogue.Count);
        int chosenThirdPredictedWeatherFillerDialogueIndex = Random.Range(0, predictedWeatherFillersDialogue.Count);
        currentText = currentWeatherFillersDialogue[chosenCurrentWeatherFillerDialogueIndex] + " " +
            currentWeathers[0].name + ".";

        currentEmotion = currentWeatherDialogue.dialogues[0].emotion;
        currentWeatherDialogue.dialogues[0].words = currentText;
        currentWeatherDialogue.dialogues[0].emotion = currentEmotion;

        currentText = "For Day " + (TimeManager.instance.dayCount + 1).ToString() + ", " +
           predictedWeatherFillersDialogue[chosenFirstPredictedWeatherFillerDialogueIndex] + " " +
           currentWeathers[1].name + ".";

        currentEmotion = currentWeatherDialogue.dialogues[1].emotion;
        currentWeatherDialogue.dialogues[1].words = currentText;
        currentWeatherDialogue.dialogues[1].emotion = currentEmotion;

        currentText = "For Day " + (TimeManager.instance.dayCount + 2).ToString() + ", " +
          predictedWeatherFillersDialogue[chosenSecondPredictedWeatherFillerDialogueIndex] + " " +
          currentWeathers[2].name + ".";

        currentEmotion = currentWeatherDialogue.dialogues[2].emotion;
        currentWeatherDialogue.dialogues[2].words = currentText;
        currentWeatherDialogue.dialogues[2].emotion = currentEmotion;

        currentText = "For Day " + (TimeManager.instance.dayCount + 3).ToString() + ", " +
            predictedWeatherFillersDialogue[chosenThirdPredictedWeatherFillerDialogueIndex] + " " +
            currentWeathers[3].name + ".";

        currentEmotion = currentWeatherDialogue.dialogues[3].emotion;
        currentWeatherDialogue.dialogues[3].words = currentText;
        currentWeatherDialogue.dialogues[3].emotion = currentEmotion;

      

        onWeatherChangedEvent?.Invoke(weathers, currentWeathers);
        PlayerManager.onUpdateCurrentRoomIDEvent.Invoke(8);
    }

    private void CheckForRoom(int id)
    {
        if (id == 3)
        {
            if (currentWeathers[0].particle != null)
            {
                currentWeathers[0].particle.Stop();
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

    public Weather GetWeathers(int p_num)
    {
        return weathers[p_num];
    }

    public Weather GetCurrentWeathers(int p_num)
    {        
        return currentWeathers[p_num];
    }

    private float GetRandNum()
    {
        float randNum = Random.Range(0, 1f);
        return randNum;
    } 
}

