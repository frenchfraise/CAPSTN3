using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject confirmPanel;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject exitTxt;

    void Start()
    {
        confirmPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        exitTxt.SetActive(false);
    }

    public void GoBack()
    {
        confirmPanel.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        exitTxt.SetActive(false);
    }

    public void ExitConfirm()
    {
        confirmPanel.SetActive(true);
        yesButton.SetActive(true);
        noButton.SetActive(true);
        exitTxt.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Time.timeScale = 1f;
    }
}
