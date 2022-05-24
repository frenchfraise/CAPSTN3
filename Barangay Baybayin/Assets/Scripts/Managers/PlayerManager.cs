using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }

            return _instance;

        }
    }
    public PlayerJoystick joystick;
    public Stamina stamina;
    public Bed bed;

    private void Awake()
    {
        //if (_instance != null)
        //{
        //    Destroy(_instance);
        //}
        //else
        //{
            _instance = this;
            //DontDestroyOnLoad(_instance);
        //}
        
    }

}
