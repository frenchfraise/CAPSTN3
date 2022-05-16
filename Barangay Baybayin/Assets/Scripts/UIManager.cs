using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public RectTransform overheadUI;
    public Image transitionUI;
    private void Awake()
    {
        instance = this;
    }

    //TEMPORARY
    public static void TransitionFade(float p_opacity)
    {
        UIManager.instance.transitionUI.DOFade(p_opacity, 0.5f);
    }
}
