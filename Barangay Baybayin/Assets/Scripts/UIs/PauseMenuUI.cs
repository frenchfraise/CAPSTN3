using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject mapGameObject;
    public GameObject settingsGameObject;
    public TutorialPanelUI tutorialPanelUI;
    public GameObject skipTutorialButton;
    
    private void Awake()
    {
        

    }
    public void BackToPauseMenuButton()
    {
        //TimeManager.onPauseGameTime.Invoke(!p_bool);
        //if (p_bool) Debug.Log("Pause Menu open!");
        //else Debug.Log("Pause Menu closed!");
        mapGameObject.SetActive(false);
        tutorialPanelUI.frame.SetActive(false);
        settingsGameObject.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    public void GoodEndingCheat()
    {
        PlayerManager.instance.GoodEndingCheat();
        settingsGameObject.SetActive(false);
        PauseMenuButton(false);
    }

    public void BadEndingCheat()
    {
        PlayerManager.instance.BadEndingCheat();
        settingsGameObject.SetActive(false);
        PauseMenuButton(false);
    }

    public void UpgradeCheat()
    {
        PlayerManager.instance.UpgradeToolCheat();
        settingsGameObject.SetActive(false);
        PauseMenuButton(false);
    }

    public void GiveCheat()
    {
        PlayerManager.instance.GiveCheat();
        settingsGameObject.SetActive(false);
        PauseMenuButton(false);
    }

    public void PauseMenuButton(bool p_bool)
    {
        //if (PlayerManager.instance.canPressPanel)
        //{
            //TimeManager.onPauseGameTime.Invoke(!p_bool);
            //if (p_bool) Debug.Log("Pause Menu open!");
            //else Debug.Log("Pause Menu closed!");
            UIManager.onGameplayModeChangedEvent.Invoke(p_bool);
            
            pauseMenuPanel.SetActive(p_bool);
        //}
       
    }

    public void QuitButton()
    {
        Debug.Log("Game quit!");
        Application.Quit();
    }
    public void SkipTutorial()
    {
        TutorialManager.instance.DontUseTutorial();
        settingsGameObject.SetActive(false);
        skipTutorialButton.SetActive(false);
        //UIManager.onGameplayModeChangedEvent.Invoke(false);
        pauseMenuPanel.SetActive(false);
    }
    public void TutorialButton(bool p_bool)
    {
        //if (p_bool) Debug.Log("Tutorial Menu open!");
        //else Debug.Log("Tutorial Menu closed!");
        tutorialPanelUI.frame.SetActive(p_bool);
        if (p_bool)
        {
            tutorialPanelUI.Open();
        }

    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button open!");
        settingsGameObject.SetActive(true);
    }

    public void MapButton(bool p_bool)
    {
        //if (p_bool) Debug.Log("Map open!");
        //else Debug.Log("Map closed!");
        mapGameObject.SetActive(p_bool);
    }
}
