using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerJoystick joystick;
    
    private void Awake()
    {
        instance = this;
    }
}
