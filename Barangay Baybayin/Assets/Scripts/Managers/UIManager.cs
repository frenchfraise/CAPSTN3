using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;


public class GameplayModeChangedEvent : UnityEvent<bool> { }
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

  


    public RectTransform overheadUI;

    public GameObject overlayCanvas;
    public GameObject gameplayHUD;
    public CharacterDialogueUI characterDialogueUI;
    public UpgradeToolsUI upgradeToolsUI;
    public Image weatherSpriteUI;
    public TransitionUI transitionUI;
    public IEnumerator runningCoroutine;

    //
    public static GameplayModeChangedEvent onGameplayModeChangedEvent = new GameplayModeChangedEvent();
    public IEnumerator proficiencyRunningCoroutine;

    private void Awake()
    {
        
        _instance = this;
        WeatherManager.onWeatherChangedEvent.AddListener(OnWeatherUIChanged); //Make a WeatherUI class and Put this in it
        onGameplayModeChangedEvent.AddListener(OnGameplayHUDSwitch);

    }

    private void OnDestroy()
    {
        WeatherManager.onWeatherChangedEvent.RemoveListener(OnWeatherUIChanged); //Make a WeatherUI class and Put this in it
        onGameplayModeChangedEvent.RemoveListener(OnGameplayHUDSwitch);
    }

  

    public static void ForceReload(GameObject p_gameObject)
    {
        UIManager.instance.StartCoroutine(UIManager.instance.Co_ForceReload(p_gameObject));
    }
    IEnumerator Co_ForceReload(GameObject p_gameObject)
    {
        p_gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.01f);
        p_gameObject.SetActive(true);
    }

    private void OnWeatherUIChanged(List<Weather> p_weathers, List<Weather> p_currentWeathers)
    {
        weatherSpriteUI.sprite = p_currentWeathers[0].sprite;
     
    }

    private void OnGameplayHUDSwitch(bool p_isActive)
    {
        TimeManager.onPauseGameTime.Invoke(!p_isActive);
        
        gameplayHUD.SetActive(!p_isActive);
        
    }


}
