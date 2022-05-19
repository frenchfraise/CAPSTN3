using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class ColorFlash
{
    [SerializeField] public Color32 flashColor;
    [SerializeField] public Color32 defaultColor;
}

[System.Serializable]
public class ColorTransition
{
    [SerializeField] public Color32 color;
    [SerializeField] public float transitionTime;
}
[System.Serializable]
public class FadeTransition
{
    [SerializeField] public float amount;
    [SerializeField] public float transitionTime;

   
}

[System.Serializable]
public class FillTransition
{
    [SerializeField] public float amount;
    [SerializeField] public float transitionTime;

 
}

[System.Serializable]
public enum BarFillUpType
{
    Increasing,
    Decreasing,
}
public class GenericBarUI : MonoBehaviour
{
    [SerializeField] private Image primaryBarUI; //colored
    [SerializeField]  private Image secondaryBarUI; //white
    
    private bool isResetting = false;
    [SerializeField] private float resetTransitionTime;

    [SerializeField] private ColorFlash colorFlash;
    [SerializeField] private List<ColorTransition> primaryBarColorTransitions = new List<ColorTransition>();

    [SerializeField] private Color32 defaultSecondaryBarColor; 
    [SerializeField] private FadeTransition secondaryBarFadeTransition;
    [SerializeField] private FillTransition secondaryBarFillTransition;

    [SerializeField] private float delayTime; 

    public void InstantUpdateBar(float p_current, float p_max)
    {
        StopAllCoroutines();
        isResetting = false;
        float fill = p_current / p_max;
        secondaryBarUI.fillAmount = fill;
        primaryBarUI.fillAmount = fill;
    }
    public void ResetBar(float p_current, float p_max)
    {
        isResetting = true;
        float fill = p_current / p_max;
        StartCoroutine(Co_UpdateBar(fill));
    }

    public void UpdateBar(float p_current, float p_max)
    {
        float fill = p_current / p_max;
        StartCoroutine(Co_UpdateBar(fill));
    }
    IEnumerator Co_UpdateBar(float p_fill)
    {
        if (!isResetting)
        {
            
            if (colorFlash != null)
            {
                primaryBarUI.color = colorFlash.flashColor;
            }

            for (int i = 0; i < primaryBarColorTransitions.Count; i++)
            {
                Tween colorTransition = primaryBarUI.DOColor(primaryBarColorTransitions[i].color, 0.05f);
                yield return colorTransition.WaitForCompletion();
            }

            if (colorFlash != null)
            {
                primaryBarUI.color = colorFlash.defaultColor;
            }
            
            primaryBarUI.fillAmount = p_fill;

            yield return new WaitForSeconds(delayTime);

            Sequence s = DOTween.Sequence();
            if (secondaryBarFadeTransition != null)
            {
                float secondaryBarFadeAmount = 0;
                if (secondaryBarFadeTransition.amount < 0)
                {
                    secondaryBarFadeAmount = p_fill;
                }
                else if (secondaryBarFadeTransition.amount > 0)
                {
                    secondaryBarFadeAmount = secondaryBarFillTransition.amount;
                }

                if (secondaryBarFadeTransition.amount < 900)
                {
                    s.Join(secondaryBarUI.DOFade(secondaryBarFadeAmount, secondaryBarFadeTransition.transitionTime));
                }
                
            }
            if (secondaryBarFillTransition != null)
            {
                float secondaryBarFillAmount = 0;
                if (secondaryBarFillTransition.amount < 0)
                {
                    secondaryBarFillAmount = p_fill;
                    Debug.Log("FILL " + secondaryBarFillTransition.transitionTime);

                }
                else if(secondaryBarFillTransition.amount > 0)
                {
                    secondaryBarFillAmount = secondaryBarFillTransition.amount;
                }
                s.Join(secondaryBarUI.DOFillAmount(secondaryBarFillAmount, secondaryBarFillTransition.transitionTime));
            }

            s.Play();
            yield return s.WaitForCompletion();
            secondaryBarUI.color = defaultSecondaryBarColor;
            
            
        }
        else
        {
            
            secondaryBarUI.fillAmount = resetTransitionTime;
            yield return new WaitForSeconds(delayTime);

            Tween maxFill = primaryBarUI.DOFillAmount(1, 0.35f);
            yield return maxFill.WaitForCompletion();

            secondaryBarUI.fillAmount = 0;
            Tween defaultFill = primaryBarUI.DOFillAmount(0, resetTransitionTime);
            yield return defaultFill.WaitForCompletion();

            isResetting = false;
            StartCoroutine(Co_UpdateBar(p_fill));
            
            
        }
        
    }

}
