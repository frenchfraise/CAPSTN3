using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

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
    public Image transitionUI;
    public CharacterDialogueUI characterDialogueUI;
    public RoomInfoUI roomInfoUI;

    public DayInfoUI dayInfoUI;
    public InventoryUI inventoryUI;
    public Image toolUseImage;
    public GameObject overlayCanvas;
    public GameObject gameplayHUD;
    public GameObject recipeUpgrade;

    public Image weatherSpriteUI;
    [Header("Weather Temporary Sprites")]
    public Sprite sunnySprite;
    public Sprite cloudySprite;
    public Sprite rainSprite;
    public Sprite stormySprite;
    public StorylinesUI storylines;
    public UpgradeToolsUI upgradeUI;
    public Coroutine runningCoroutine;

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
        WeatherManager.onWeatherChangedEvent.AddListener(OnWeatherUIChanged);
    }

    private void OnDisable()
    {
        WeatherManager.onWeatherChangedEvent.RemoveListener(OnWeatherUIChanged);
    }

    //TEMPORARY
    private void OnWeatherUIChanged(Weather p_weather)
    {
        switch (p_weather)
        {
            case Weather.Sunny:                
                weatherSpriteUI.sprite = sunnySprite;
                FindObjectOfType<AudioManager>().Play("Sunny");
                break;
            case Weather.Cloudy:
                weatherSpriteUI.sprite = cloudySprite;
                FindObjectOfType<AudioManager>().Play("Cloudy");
                break;
            case Weather.Rainy:
                weatherSpriteUI.sprite = rainSprite;
                FindObjectOfType<AudioManager>().Play("Rain");
                break;
            case Weather.Stormy:
                weatherSpriteUI.sprite = stormySprite;
                FindObjectOfType<AudioManager>().Play("Storm");
                break;
        }
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
