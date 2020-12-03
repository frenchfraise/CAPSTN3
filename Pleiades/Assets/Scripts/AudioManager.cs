using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;

    public AudioSource boxBreak;
    public AudioSource doorUnlock;

    public AudioSource playerHit;
    public AudioSource enemyProj;

    public AudioSource lightning;
    public AudioSource fireBall;
    public AudioSource slash;

    public AudioSource gemGet;
    public AudioSource gemSwap;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
