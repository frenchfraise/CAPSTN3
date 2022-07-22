using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuUI : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject MapGameObject;
    public GameObject TutorialPanel;
    public TutorialPanelUI tutorialPanelUI;

    private void Awake()
    {
        

    }
    public void BackToPauseMenuButton()
    {
        //TimeManager.onPauseGameTime.Invoke(!p_bool);
        //if (p_bool) Debug.Log("Pause Menu open!");
        //else Debug.Log("Pause Menu closed!");
        MapGameObject.SetActive(false);
        TutorialPanel.SetActive(false);
        PauseMenuPanel.SetActive(true);
    }
    public void PauseMenuButton(bool p_bool)
    {
        //TimeManager.onPauseGameTime.Invoke(!p_bool);
        //if (p_bool) Debug.Log("Pause Menu open!");
        //else Debug.Log("Pause Menu closed!");
        UIManager.onGameplayModeChangedEvent.Invoke(p_bool);
        PauseMenuPanel.SetActive(p_bool);
    }

    public void QuitButton()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }
    public void SkipTutorial()
    {
        TutorialManager.instance.DontUseTutorial();
        UIManager.onGameplayModeChangedEvent.Invoke(false);
        PauseMenuPanel.SetActive(false);
    }
    public void TutorialButton(bool p_bool)
    {        
        //if (p_bool) Debug.Log("Tutorial Menu open!");
        //else Debug.Log("Tutorial Menu closed!");
        TutorialPanel.SetActive(p_bool);
        tutorialPanelUI.Open();
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button open!");
    }

    public void MapButton(bool p_bool)
    {
        //if (p_bool) Debug.Log("Map open!");
        //else Debug.Log("Map closed!");
        MapGameObject.SetActive(p_bool);
    }
}
