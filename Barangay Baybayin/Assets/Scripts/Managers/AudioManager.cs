using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    private bool isFirstTime = true;
    private Passageway currentAudioPassageway;
    [NonReorderable] public Sound[] sounds;
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }        
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("SFX"))
        {
            mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        }
    }
    private void OnEnable()
    {
        PlayerManager.onRoomEnteredEvent.AddListener(PlayOnRoomEnter);
        // PlayerManager.onUpdateCurrentRoomIDEvent.AddListener(PlayOnRoomEnter);
    }

    private void OnDisable()
    {
        PlayerManager.onRoomEnteredEvent.RemoveListener(PlayOnRoomEnter);
        //PlayerManager.onUpdateCurrentRoomIDEvent.RemoveListener(PlayOnRoomEnter);
    }

    public void PlayByName(string name)
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
        //sounds[id].source.Play();
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    private void PlayOnRoomEnter(Passageway p_passageway)
    {
        if (!isFirstTime)
        {
            if (p_passageway.room.roomDescription != currentAudioPassageway.room.roomDescription) 
                isFirstTime = true;
        }
        if (isFirstTime)
        {
            isFirstTime = false;
            PlayByName(p_passageway.room.roomDescription);
        }
        currentAudioPassageway = p_passageway;
    }
  
}
