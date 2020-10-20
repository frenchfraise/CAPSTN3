using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        Pause.GameIsPaused = false;
    }
}
