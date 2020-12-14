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
    public GameObject skills;
    public GameObject gems;
    public GameObject confirmPanel;
    public KeyCode esc;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(esc))
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        skills.SetActive(false);
        gems.SetActive(false);
    }

}
