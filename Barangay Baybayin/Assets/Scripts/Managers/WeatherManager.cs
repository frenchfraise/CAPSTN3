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

    [SerializeField] private Weather currentWeather = Weather.Stormy;
    public Weather CurrentWeather => currentWeather;

    [SerializeField] ParticleSystem cloudParticles;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] ParticleSystem lightningParticles;
    
    private void Awake()
    {
        _instance = this;
    }

    // TEMPORARILY stops everything at Start, for sureness
    private void Start()
    {
        cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        StartCoroutine(WeatherCheckTimer());
    }

    IEnumerator WeatherCheckTimer()
    {
        while (true)
        {
            onWeatherChangedEvent?.Invoke(currentWeather);
            yield return new WaitForSeconds(10f);

            switch (currentWeather)
            {
                case Weather.Sunny:
                    cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    FindObjectOfType<AudioManager>().Play("Sunny");
                    break;
                case Weather.Cloudy:
                    cloudParticles.Play();
                    rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    FindObjectOfType<AudioManager>().Play("Cloudy");
                    break;
                case Weather.Rainy:
                    cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    rainParticles.Play();
                    lightningParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    FindObjectOfType<AudioManager>().Play("Rain");
                    break;
                case Weather.Stormy:
                    cloudParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    lightningParticles.Play();
                    FindObjectOfType<AudioManager>().Play("Storm");
                    break;
            }
        }
    }
}

public enum Weather
{
    Sunny,
    Cloudy,
    Rainy,
    Stormy
}
