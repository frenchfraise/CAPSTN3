using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject MapGameObject;

    public void PauseMenuButton(bool p_bool)
    {
        //TimeManager.onPauseGameTime.Invoke(!p_bool);
        if (p_bool) Debug.Log("Pause Menu open!");
        else Debug.Log("Pause Menu closed!");
        PauseMenuPanel.SetActive(p_bool);
    }

    public void QuitButton()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }

    public void TutorialButton()
    {
        Debug.Log("Tutorial Button open!");
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button open!");
    }

    public void MapButton(bool p_bool)
    {
        if (p_bool) Debug.Log("Map open!");
        else Debug.Log("Map closed!");
        MapGameObject.SetActive(p_bool);
    }
}
