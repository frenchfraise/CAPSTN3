using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        Pause.GameIsPaused = false;
    }
}
