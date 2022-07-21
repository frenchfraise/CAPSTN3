using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject confirmPanel;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject restartTxt;

    void Start()
    {
        confirmPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        restartTxt.SetActive(false);
    }

    public void GoBack()
    {
        confirmPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        restartTxt.SetActive(false);
    }

    public void RestartConfirm()
    {
        confirmPanel.SetActive(true);
        yesButton.SetActive(true);
        noButton.SetActive(true);
        restartTxt.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
