using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingControl : MonoBehaviour
{
    [SerializeField] private SO_LightingSchedule lightingSchedule;

    private Light2D light2D;    

    private void Awake()
    {
        // Get 2D light
        light2D = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        TimeManager.onTimeChangedEvent.AddListener(OnTimeCheck);
    }

    private void OnDisable()
    {
        TimeManager.onTimeChangedEvent.RemoveListener(OnTimeCheck);
    }

    private void OnTimeCheck(int p_hour, int p_minute, int p_minuteByTens)
    {
        SetLightingIntensity(p_hour);
    }

    private void SetLightingIntensity(int p_hour)
    {
        for (int i = 0; i < lightingSchedule.lightingBrightnessArray.Length; i++)
        {
            if (lightingSchedule.lightingBrightnessArray[i].hour == p_hour)
            {
                light2D.intensity = lightingSchedule.lightingBrightnessArray[i].lightIntensity;
                break;
            }
        }
    }
}
