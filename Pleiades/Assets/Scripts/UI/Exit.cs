using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1f;
        Pause.GameIsPaused = false;
    }
}
