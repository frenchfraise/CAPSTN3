using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using DG.Tweening;

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
   // private Passageway currentAudioPassageway;
    [NonReorderable] public Sound[] sounds;
    public AudioMixer mixer;
    string currentSongPlaying = "";
    string previousSongPlaying = "";
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = s.output;
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

    //public void PlayByName(string name)
    //{
    //    foreach (Sound s in sounds)
    //    {
    //        s.source.Stop();
    //    }
    //    Sound sound = Array.Find(sounds, sound => sound.name == name);
    //    sound.source.Play();
    //    sound.source.volume = 1f;
    //}

    public Sound GetSoundByName(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        return sound;
    }

    private void PlayOnRoomEnter(Passageway p_passageway)
    {
        if (!isFirstTime)
        {
            if (p_passageway.room.roomDescription != currentSongPlaying) 
                isFirstTime = true;
        }
        if (isFirstTime)
        {
            isFirstTime = false;
            previousSongPlaying = currentSongPlaying;
            currentSongPlaying = p_passageway.room.roomDescription;
            StartCoroutine(Co_AudioFade());

            //PlayByName(p_passageway.room.roomDescription);
        }
        
    }
  
    IEnumerator Co_AudioFade()
    {
        Sound sound;
        string soundName = previousSongPlaying;
        sound = GetSoundByName(soundName);
        //Debug.Log("PLAYYYINNG " + soundName);
        if (soundName != "")
        {
            Sequence wee = DOTween.Sequence();
            wee.Append(sound.source.DOFade(0, 1.25f));
            wee.Play();
            yield return wee.WaitForCompletion();
        }
    

 
        //sound = GetSoundByName(soundName);
        //Sequence wee = DOTween.Sequence();
        //wee.Append(sound.source.DOFade(0, 1.25f));
        //wee.Play();
        //yield return wee.WaitForCompletion();
        //while (timeElapsed < timeToFade)
        //{            
        //    sound.volume = Mathf.Lerp(0, 0.5f, timeElapsed / timeToFade);
        //    timeElapsed += Time.deltaTime;
        //    yield return null;
        //}
        Sound currentSound;
        string currentSoundName = currentSongPlaying;
        currentSound = GetSoundByName(currentSoundName);
        //Debug.Log("NEWWWW PLAYYYINNG " + currentSoundName);
        currentSound.source.Play();
        if (currentSoundName != "")
        {
            Sequence wee2 = DOTween.Sequence();
            wee2.Append(currentSound.source.DOFade(1, 1.25f));
            wee2.Play();
        }

        // yield return wee2.WaitForCompletion();
    }

    public void StartCoFade(string song1, string song2)
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
        StartCoroutine(Co_AudioFade2(song1, song2));
    }

    public IEnumerator Co_AudioFade2(string song1, string song2)
    {
        Sound sound;
        string soundName = song1;
        sound = GetSoundByName(soundName);
        if (soundName != "")
        {
            Sequence wee = DOTween.Sequence();
            wee.Append(sound.source.DOFade(0, 1.25f));
            wee.Play();
            yield return wee.WaitForCompletion();
        }

        Sound currentSound;
        string currentSoundName = song2;
        currentSound = GetSoundByName(currentSoundName);
        currentSound.source.Play();
        if (currentSoundName != "")
        {
            Sequence wee2 = DOTween.Sequence();
            wee2.Append(currentSound.source.DOFade(1, 1.25f));
            wee2.Play();
        }
    }
}
