using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightingControl : MonoBehaviour
{
    [SerializeField] private SO_LightingSchedule lightingSchedule;

    private Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        //TimeManager.onTimeChangedEvent.AddListener(OnTimeCheck);
        //WeatherManager.onWeatherChangedEvent.AddListener(GetWeather);
        TimeManager.onHourChanged.AddListener(OnTimeCheck);
    }

    private void OnDisable()
    {
        //TimeManager.onTimeChangedEvent.RemoveListener(OnTimeCheck);
        TimeManager.onHourChanged.RemoveListener(OnTimeCheck);
    }

    private void OnTimeCheck(int p_hour)
    {
        SetLightingIntensity(p_hour);
    }

    private void SetLightingIntensity(int p_hour)
    {
        for (int i = 0; i < lightingSchedule.lightingBrightnessArray.Length; i++)
        {
            if (WeatherManager.instance.GetCurrentWeathers(0).name == lightingSchedule.lightingBrightnessArray[i].weatherName)
            {
                if (lightingSchedule.lightingBrightnessArray[i].hour == p_hour)
                {
                    float targetLightingIntensity = lightingSchedule.lightingBrightnessArray[i].lightIntensity;
                    Color targetColor = lightingSchedule.lightingBrightnessArray[i].color;
                    StartCoroutine(FadeLightRoutine(targetLightingIntensity, targetColor));
                    Debug.Log("Lighting working!");
                    break;
                }
            }
        }
    }

    private IEnumerator FadeLightRoutine(float targetLightingIntensity, Color targetColor)
    {
        float fadeDuration = 5f;
        float fadeSpeed = Mathf.Abs(light2D.intensity - targetLightingIntensity) / fadeDuration;
        while (light2D.intensity != targetLightingIntensity && light2D.color != targetColor)
        {
            // Debug.Log(light2D.color + " != " + targetColor);
            light2D.intensity = Mathf.MoveTowards(light2D.intensity, targetLightingIntensity, fadeSpeed * Time.deltaTime);           
            light2D.color = Color.Lerp(light2D.color, targetColor, 0.01f);
            if (light2D.intensity == targetLightingIntensity && light2D.color == targetColor)
            {
                light2D.intensity = targetLightingIntensity;
                light2D.color = targetColor;
                break;
            }
            yield return null;
        }        
        light2D.intensity = targetLightingIntensity;
        light2D.color = targetColor;
    }
    //!Mathf.Approximately(light2D.intensity, targetLightingIntensity)
}
