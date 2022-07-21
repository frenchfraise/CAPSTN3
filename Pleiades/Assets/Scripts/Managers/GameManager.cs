using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public Player player;
    public PlayerController playerController;
    public Slider healthBar;
    public Gem gem;
    public Skills skills;

    void Awake()
    {
        _instance = this;
        
    }
}
