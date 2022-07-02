using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [NonReorderable] public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }        
    }

    private void OnEnable()
    {
        //WeatherManager.instance.onWeatherChangedEvent.AddListener(Play);
    }

    private void OnDisable()
    {
        //WeatherManager.instance.onWeatherChangedEvent.RemoveListener(Play);
    }
    // public void Play(Weather p_weather, int id)
    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
        //sounds[id].source.Play();
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();

    }

  
}
