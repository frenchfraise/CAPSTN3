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

  
    public Image transitionUI;

    public Image toolUseImage;

    public RectTransform overheadUI;

    public GameObject overlayCanvas;
    public GameObject gameplayHUD;

    public Image weatherSpriteUI; //Make a WeatherUI class and Put this in it

    public Coroutine runningCoroutine;

    //
    public static GameplayModeChangedEvent onGameplayModeChangedEvent = new GameplayModeChangedEvent();
    
    public bool isRunningCoroutine = false;
    public bool justFinishedCoroutine = false;
    private void Awake()
    {
        if (_instance != null)
        {
            //Destroy(gameObject);
            
        }
        else
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
            
        }

      
    }

    private void OnEnable()
    {
        WeatherManager.onWeatherChangedEvent.AddListener(OnWeatherUIChanged); //Make a WeatherUI class and Put this in it
        onGameplayModeChangedEvent.AddListener(OnGameplayHUDSwitch);

    }

    private void OnDisable()
    {
        WeatherManager.onWeatherChangedEvent.RemoveListener(OnWeatherUIChanged); //Make a WeatherUI class and Put this in it
        onGameplayModeChangedEvent.RemoveListener(OnGameplayHUDSwitch);
   
    }

    //TEMPORARY Make a WeatherUI class and Put this in it
    private void OnWeatherUIChanged(Weather p_currentWeather, Weather p_nextWeather)
    {
        weatherSpriteUI.sprite = p_currentWeather.sprite;
        FindObjectOfType<AudioManager>().Play(p_currentWeather.audioName);
       
    }

    private void OnGameplayHUDSwitch(bool p_bool)
    {
        TimeManager.onPauseGameTime.Invoke(p_bool);
        p_bool = !p_bool;
        gameplayHUD.SetActive(p_bool);
        
    }

    //TEMPORARY
    public static void TransitionFade(float p_opacity, bool p_isActiveOnEnd = true)
    {
        UIManager.instance.StartCoroutine(UIManager.instance.Co_TransitionFade(p_opacity, p_isActiveOnEnd));
    }
    public IEnumerator Co_TransitionFade(float p_opacity, bool p_isActiveOnEnd)
    {

        UIManager.instance.transitionUI.gameObject.SetActive(true);
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Join(UIManager.instance.transitionUI.DOFade(p_opacity, 0.5f));

        yield return fadeSequence.WaitForCompletion();
        UIManager.instance.transitionUI.gameObject.SetActive(p_isActiveOnEnd);

    }
    public static void TransitionPreFadeAndPostFade(float p_preOpacity,
                                            float p_preTransitionTime,
                                            float p_delayTime,
                                            float p_postOpacity,
                                            float p_postTransitionTime,
                                            Action p_preAction = null,
                                            Action p_postAction = null)
    {
        UIManager.instance.StartCoroutine(UIManager.instance.Co_TransitionPreFadeAndPostFade(p_preOpacity,
                                                                                            p_preTransitionTime,
                                                                                            p_delayTime,
                                                                                            p_postOpacity,
                                                                                            p_postTransitionTime,
                                                                                            p_preAction,
                                                                                            p_postAction));
    }

    public IEnumerator Co_TransitionPreFadeAndPostFade(float p_preOpacity, 
                            float p_preTransitionTime,
                            float p_delayTime,
                            float p_postOpacity,
                            float p_postTransitionTime,
                            Action p_preAction = null,
                            Action p_postAction = null)
    {
        Sequence preSequence = DOTween.Sequence();
        UIManager.instance.transitionUI.gameObject.SetActive(true);
        preSequence.Join(UIManager.instance.transitionUI.DOFade(p_preOpacity, p_preTransitionTime));

        yield return preSequence.WaitForCompletion();

        p_preAction?.Invoke();
        yield return new WaitForSeconds(p_delayTime);
        Sequence postSequence = DOTween.Sequence();
        postSequence.Join(UIManager.instance.transitionUI.DOFade(p_postOpacity, p_postTransitionTime));
        yield return postSequence.WaitForCompletion();

        UIManager.instance.transitionUI.gameObject.SetActive(false);
        p_postAction?.Invoke();
    }
}
