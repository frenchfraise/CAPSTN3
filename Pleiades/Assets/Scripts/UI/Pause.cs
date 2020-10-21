using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;
    public static bool GameIsPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    public void PauseMenu()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
