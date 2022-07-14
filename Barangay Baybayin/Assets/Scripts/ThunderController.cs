 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class ThunderController : MonoBehaviour
{
    private Light2D thunderLight2D;
    private Coroutine thunderCoroutine;

    private void Awake()
    {
        thunderLight2D = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        // WeatherManager.onWeatherChangedEvent.AddListener(ThunderTimerCheck);
    }

    public void ThunderTimerCheck(List<Weather> p_weather, List<Weather> p_currentWeathers)
    {        
        //if (p_currentWeathers[0].name == "Stormy")
        thunderCoroutine = StartCoroutine(ThunderFlicker(true));
        //else StopCoroutine(ThunderFlicker(false));
    }

    public IEnumerator ThunderFlicker(bool p_bool)
    {
        while (p_bool)
        {
            // Frequency
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            
            thunderLight2D.intensity = Random.Range(0.1f, 1f);
            thunderLight2D.enabled = true;
            
            // Play sound

            // Flash speed
            yield return new WaitForSeconds(Random.Range(0.25f, 0.5f));
            thunderLight2D.enabled = false;

            // Double Flash 50/50 chance            
            if (0.5f >= Random.Range(0f, 1f))
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f));
                thunderLight2D.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.25f, 0.5f));
                thunderLight2D.enabled = false;
            }
        }
    }
}
