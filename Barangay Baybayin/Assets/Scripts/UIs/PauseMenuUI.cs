using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject mapGameObject;
    public TutorialPanelUI tutorialPanelUI;
    public GameObject skipTutorialButton;

    [Header("Settings")]
    public GameObject settingsGameObject;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Text volumeText;
    public Button enableButton;
    public Button disableButton;
    public Sprite enabledSprite;
    public Sprite disabledSprite;

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
        //TimeManager.onPauseGameTime.Invoke(!p_bool);
        //if (p_bool) Debug.Log("Pause Menu open!");
        //else Debug.Log("Pause Menu closed!");
        UIManager.onGameplayModeChangedEvent.Invoke(p_bool);

        pauseMenuPanel.SetActive(p_bool);
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

    #region Settings
    public void SettingsButton()
    {
        //Debug.Log("Settings Button open!");
        settingsGameObject.SetActive(true);
    }
    public void OnVolumeSliderChange(float value)
    {
        volumeText.text = ((int)(value * 100)).ToString();
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }
    public void OnVolumeButtonsClicked(bool p_bool)
    {
        if (p_bool)
        {
            enableButton.GetComponentInChildren<TMP_Text>().color = Color.white;
            enableButton.image.sprite = enabledSprite;

            disableButton.GetComponentInChildren<TMP_Text>().color = Color.black;
            disableButton.image.sprite = disabledSprite;

            volumeSlider.value = volumeSlider.maxValue;
            volumeText.text = ((int)volumeSlider.maxValue * 100).ToString();
            audioMixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            disableButton.GetComponentInChildren<TMP_Text>().color = Color.white;
            disableButton.image.sprite = enabledSprite;

            enableButton.GetComponentInChildren<TMP_Text>().color = Color.black;
            enableButton.image.sprite = disabledSprite;

            volumeSlider.value = volumeSlider.minValue;
            volumeText.text = ((int)volumeSlider.minValue * 100).ToString();
            audioMixer.SetFloat("MasterVolume", -80);
        }
    }
    #endregion

    public void MapButton(bool p_bool)
    {
        //if (p_bool) Debug.Log("Map open!");
        //else Debug.Log("Map closed!");
        mapGameObject.SetActive(p_bool);
    }
}
