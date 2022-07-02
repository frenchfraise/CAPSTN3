using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.Audio;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    //public AudioSource sound;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;
}
