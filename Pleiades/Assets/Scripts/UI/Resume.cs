using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject skills;
    public GameObject gems;

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        skills.SetActive(true);
        gems.SetActive(true);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
