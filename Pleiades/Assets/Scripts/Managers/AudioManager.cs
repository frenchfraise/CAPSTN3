using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;

    public AudioSource boxBreak;
    public AudioSource doorUnlock;

    public AudioSource hpItemGet;
    public AudioSource playerHit;
    public AudioSource enemyProj;
    public AudioSource enemyGotHit;

    public AudioSource lightning;
    public AudioSource fireBall;
    public AudioSource slash;

    public AudioSource gemGet;
    public AudioSource gemSwap;

    public static AudioManager Instance;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

///Potion Drink Regen Copyright 2012 DrMinky
///Health Potion Copyright 2012 Iwan Gabovitch, CC-BY3 license.