using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;

    public void Resume()
    {
        pausePanel.SetActive(false);
    }

    public void Restart()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
