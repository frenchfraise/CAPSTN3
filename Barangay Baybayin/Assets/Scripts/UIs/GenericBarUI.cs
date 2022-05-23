using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public class InstantColorData : UnityTransitionData
{
    [SerializeField] public Color32 color;
    public override void PerformTransition()
    {
        image.color = color;
    }

}

[System.Serializable]
public class ColorTransitionData: DoTweenTransitionData
{
    [SerializeField] public Color32 amount;
    [SerializeField] public float transitionTime;
    public override Tween GetAndPerformTween()
    {
        Tween colorTransition = image.DOColor(amount, transitionTime);
        return colorTransition;
    }

}
[System.Serializable]
public class FadeTransitionData: DoTweenTransitionData
{
    [SerializeField] public float amount;
    [SerializeField] public float transitionTime;
    public override Tween GetAndPerformTween()
    {
        Tween colorTransition = image.DOFade(amount, transitionTime);
        return colorTransition;
    }
}

[System.Serializable]
public class FillTransitionData: DoTweenTransitionData
{
    [SerializeField] public float amount;
    [SerializeField] public float transitionTime;
    public override Tween GetAndPerformTween()
    {
        Tween colorTransition = image.DOFillAmount(amount, transitionTime);
        return colorTransition;
    }
}

[System.Serializable]
public class DoTweenTransitionData : TransitionData
{
    [SerializeField] public bool joinToNextTransition;
    [SerializeField] public bool waitToFinish;
    [SerializeField] public float delayTimeToNextTransition;
    public virtual Tween GetAndPerformTween()
    {
        return null;
    }
}

[System.Serializable]
public class UnityTransitionData : TransitionData
{

    public override void PerformTransition()
    {

    }
}

[System.Serializable]
public class TransitionData
{
    [SerializeField] public Image image;

    public virtual void PerformTransition()
    {

    }
}

[System.Serializable]
public class TransitionsDataHolder
{
    //[System.Serializable]
    [SerializeField]
    //public string name;
    public List<TransitionData> transitionDatas = new List<TransitionData>();
    public void PerformTransitionsData()
    {
        UIManager.instance.StartCoroutine(Co_PerformingTransitionsData()); //change this
    }

    IEnumerator Co_PerformingTransitionsData()
    {
        Sequence transitionSequence = DOTween.Sequence();
        for (int i = 0; i < transitionDatas.Count; i++)
        {

            TransitionData currentTransitionData = transitionDatas[i];
            if (currentTransitionData is UnityTransitionData)
            {
                UnityTransitionData currentUnityTransitionData = currentTransitionData as UnityTransitionData;
                currentUnityTransitionData.PerformTransition();
            }
            else if (currentTransitionData is DoTweenTransitionData)
            {
                DoTweenTransitionData currentDoTweenTransitionData = currentTransitionData as DoTweenTransitionData;
                Tween transition = currentDoTweenTransitionData.GetAndPerformTween();
                if (currentDoTweenTransitionData.joinToNextTransition)
                {
                    if (transitionSequence != null)
                    {
                        transitionSequence.Join(transition);
                    }
                    else
                    {
                        transitionSequence = DOTween.Sequence();
                    }

                }
                else
                {
                    if (transitionSequence != null)
                    {

                        if (currentDoTweenTransitionData.waitToFinish)
                        {
                            yield return transitionSequence.WaitForCompletion();

                        }
                        else if (!currentDoTweenTransitionData.waitToFinish)
                        {
                            yield return new WaitForSeconds(currentDoTweenTransitionData.delayTimeToNextTransition);
                        }
                        transitionSequence = null;
                    }
                    else
                    {
                        if (currentDoTweenTransitionData.waitToFinish)
                        {

                            yield return transition.WaitForCompletion();


                        }
                        else if (!currentDoTweenTransitionData.waitToFinish)
                        {
                            yield return new WaitForSeconds(currentDoTweenTransitionData.delayTimeToNextTransition);
                        }
                    }
                }
            }
        }
    }
}
[System.Serializable]
public enum BarFillUpType
{
    Increasing,
    Decreasing,
}
public class GenericBarUI : MonoBehaviour
{
    [SerializeField] private Image realBarUI; //colored
    [SerializeField] private Image ghostBarUI; //white
    [SerializeField] private Image restrictedBarUI; //red

    private bool isResetting = false;
    [SerializeField] private float resetTransitionTime;


    //public List<TransitionsDataHolder> transitionsDataHolders;// = new List<TransitionsDataHolder>();

    [SerializeField] private Color32 defaultRealBarColor;
    [SerializeField] private InstantColorData realBarColorFlash;
    [SerializeField] private List<ColorTransitionData> realBarColorTransitions = new List<ColorTransitionData>();

    [SerializeField] private Color32 defaultGhostBarColor;
    [SerializeField] private FadeTransitionData ghostBarFadeTransition;
    [SerializeField] private FillTransitionData ghostBarFillTransition;

    [SerializeField] private float delayTime;

    public void InstantUpdateBar(float p_current, float p_currentMax, float p_max)
    {
        StopAllCoroutines();
        isResetting = false;
        float fill = p_current / p_max;
        
        float restrictedFill = (p_max/ p_max) - (p_currentMax / p_max);
        if (realBarUI)
        {
            realBarUI.fillAmount = fill;
        }
        else
        {
            Debug.LogError(gameObject.name.ToString() + " IS MISSING primaryBarUI REFERENCE IN INSPECTOR");
        }

        if (ghostBarUI)
        {
            ghostBarUI.fillAmount = fill;
        }
        else
        {
            Debug.LogError(gameObject.name.ToString() + " IS MISSING ghostBarUI REFERENCE IN INSPECTOR");
        }

        if (restrictedBarUI)
        {
            restrictedBarUI.fillAmount = restrictedFill;
        }
        else
        {
           // Debug.LogError(gameObject.name.ToString() + " IS MISSING restrictedBarUI REFERENCE IN INSPECTOR");
        }
        
    }
    public void ResetBar(float p_current, float p_currentMax)
    {
        isResetting = true;
        float fill = p_current / p_currentMax;
        StartCoroutine(Co_UpdateBar(fill));
    }

    public void UpdateBar(float p_current, float p_currentMax )
    {
        float fill = p_current / p_currentMax;

        StartCoroutine(Co_UpdateBar(fill));
    }
    IEnumerator Co_UpdateBar(float p_fill)
    {
        if (!isResetting)
        {
            //foreach (TransitionsDataHolder currentTransitionsDataHolder in transitionsDataHolders)
            //{
            //    currentTransitionsDataHolder.PerformTransitionsData();
            //    yield return new WaitForSeconds(delayTime);
            //}


            if (realBarColorFlash != null)
            {
                realBarUI.color = realBarColorFlash.color;
            }

            if (realBarColorFlash != null)
            {
                realBarUI.color = defaultRealBarColor;
            }
            if (realBarColorFlash != null)
            {
                realBarUI.color = realBarColorFlash.color;
            }

            for (int i = 0; i < realBarColorTransitions.Count; i++)
            {
                Tween colorTransition = realBarUI.DOColor(realBarColorTransitions[i].amount, 0.05f);
                yield return colorTransition.WaitForCompletion();
            }

            if (realBarColorFlash != null)
            {
                realBarUI.color = defaultRealBarColor;
            }

            realBarUI.fillAmount = p_fill;

            yield return new WaitForSeconds(delayTime);

            Sequence s = DOTween.Sequence();
            if (ghostBarFadeTransition != null)
            {
                float secondaryBarFadeAmount = 0;
                if (ghostBarFadeTransition.amount < 0)
                {
                    secondaryBarFadeAmount = p_fill;
                }
                else if (ghostBarFadeTransition.amount > 0)
                {
                    secondaryBarFadeAmount = ghostBarFillTransition.amount;
                }

                if (ghostBarFadeTransition.amount < 900)
                {
                    s.Join(ghostBarUI.DOFade(secondaryBarFadeAmount, ghostBarFadeTransition.transitionTime));
                }

            }
            if (ghostBarFillTransition != null)
            {
                float secondaryBarFillAmount = 0;
                if (ghostBarFillTransition.amount < 0)
                {
                    secondaryBarFillAmount = p_fill;


                }
                else if (ghostBarFillTransition.amount > 0)
                {
                    secondaryBarFillAmount = ghostBarFillTransition.amount;
                }
                s.Join(ghostBarUI.DOFillAmount(secondaryBarFillAmount, ghostBarFillTransition.transitionTime));
            }

            s.Play();
            yield return s.WaitForCompletion();
            ghostBarUI.color = defaultGhostBarColor;


        }
        else
        {
            
            ghostBarUI.fillAmount = resetTransitionTime;
            yield return new WaitForSeconds(delayTime);

            Tween maxFill = realBarUI.DOFillAmount(1, 0.35f);
            yield return maxFill.WaitForCompletion();

            ghostBarUI.fillAmount = 0;
            Tween defaultFill = realBarUI.DOFillAmount(0, resetTransitionTime);
            yield return defaultFill.WaitForCompletion();

            isResetting = false;
            StartCoroutine(Co_UpdateBar(p_fill));
            
            
        }
        
    }

}
