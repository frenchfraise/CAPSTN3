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
        bar.color = color;
        
    }

}

[System.Serializable]
public class ColorTransitionData: DoTweenTransitionData
{
    [SerializeField] public Color32 amount;
    [SerializeField] public float transitionTime;
    public override Tween GetAndPerformTween()
    {
        Tween colorTransition = bar.DOColor(amount, transitionTime);
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
        Tween colorTransition = bar.DOFade(amount, transitionTime);
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
        Tween colorTransition = bar.DOFillAmount(amount, transitionTime);
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
public enum TransitionType
{
    None,
    InstantColorData,
    FadeTransitionData,
    FillTransitionData,
    DoTweenTransitionData,
}
[System.Serializable]
public class TransitionData
{
    
    [SerializeField]
    public TransitionType transitionType;
    [SerializeField] public Image bar;

 
    public virtual void PerformTransition()
    {

    }
}

[System.Serializable]
public class TransitionsDataHolder
{
 
    [SerializeField] public List<TransitionData> transitionDatas = new List<TransitionData>();
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

public class GenericBarUI : MonoBehaviour
{

    [SerializeField] private Image realBarUI; //colored
    [SerializeField] private Image ghostBarUI; //white
    [SerializeField] private Image restrictedBarUI; //red

    private bool isResetting = false;
    private bool isCurrentlyResettingCoroutine = false;
    private bool isUpdateNext = false;
    [SerializeField] private float resetTransitionTime;


    [SerializeField] public List<TransitionsDataHolder> transitionsDataHolders;// = new List<TransitionsDataHolder>();

    [SerializeField] private Color32 defaultRealBarColor;
    [SerializeField] private InstantColorData realBarColorFlash;
    [SerializeField] private List<ColorTransitionData> realBarColorTransitions = new List<ColorTransitionData>();

    [SerializeField] private Color32 defaultGhostBarColor;
    [SerializeField] private FadeTransitionData ghostBarFadeTransition;
    [SerializeField] private FillTransitionData ghostBarFillTransition;

    [SerializeField] private float delayTime = 0;

    private float current = 0;
    private float currentMax = 1;
    private float savedFill = 0;
    IEnumerator runningCoroutine;

    private void Awake()
    {
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);
 
    }
    private void OnDestroy()
    {
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
    }
    private void OnEnable()
    {
        realBarUI.fillAmount = 0f;
        ghostBarUI.fillAmount = 0f;
        restrictedBarUI.fillAmount = 0f;
    }

    private void OnDisable()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        InstantUpdateBar(savedFill);
    }

    void GameplayModeChangedEvent(bool p_set)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        realBarUI.fillAmount = savedFill;
        ghostBarUI.fillAmount = savedFill;
        //restrictedBarUI.fillAmount = savedFill;
        //Debug.Log(gameObject.name + " - " + current + " - " + currentMax);
        InstantUpdateBar(current, currentMax, currentMax);

        

    }

    public bool test(int p_tdhi, int p_int, int p_enumType)
    {

        switch (p_enumType)
        {
            case 0:

                break;
            case 1:
                InstantColorData instantColorData = new InstantColorData();
                transitionsDataHolders[p_tdhi].transitionDatas[p_int] = instantColorData;
                Debug.Log("InstantColorData");

                break;

            case 2:
                ColorTransitionData colorTransitionData = new ColorTransitionData();
                transitionsDataHolders[p_tdhi].transitionDatas[p_int] = colorTransitionData;
                break;

            case 3:
                FadeTransitionData fadeTransitionData = new FadeTransitionData();
                transitionsDataHolders[p_tdhi].transitionDatas[p_int] = fadeTransitionData;
                break;

            case 4:
                FillTransitionData fillTransitionData = new FillTransitionData();
                transitionsDataHolders[p_tdhi].transitionDatas[p_int] = fillTransitionData;
                break;
        }
        int count = 0;
        foreach (TransitionData td in transitionsDataHolders[p_tdhi].transitionDatas)
        {
            
            if (td is InstantColorData )
            {
                InstantColorData tes = (InstantColorData)td;
                Debug.Log(count + " - InstantColorData" + tes.color.ToString());
            }
            else if(td is ColorTransitionData)
            {
                Debug.Log(count + " - ColorTransitionData");
            }
            else if (td is FadeTransitionData)
            {
                Debug.Log(count + " - FadeTransitionData");
            }
            else if (td is FillTransitionData)
            {
                Debug.Log(count + " - FillTransitionData");
            }
            count++;
        }
        return true;
    }
    public void InstantUpdateBar(float p_current =0, float p_currentMax = 1, float p_max =1)
    {
        //Debug.Log("INSTANT: " + p_current + " " + p_currentMax);
        StopAllCoroutines();
        isResetting = false;

        current = p_current;
        currentMax = p_max;
        float fill = p_current / p_max;
        savedFill = fill;
       // Debug.Log("INSTAAAAA: " + savedFill + " - " + p_current + " - " + p_max) ;
        //float restrictedFill = (p_max/ p_max) - (p_currentMax / p_max);
        if (realBarUI)
        {
            //Debug.Log(savedFill);
            realBarUI.DOFillAmount(savedFill,0.01f);
            realBarUI.color = new Color(realBarUI.color.r, realBarUI.color.g, realBarUI.color.b, 1);
        }
        else
        {
            Debug.LogError(gameObject.name.ToString() + " IS MISSING primaryBarUI REFERENCE IN INSPECTOR");
        }

        if (ghostBarUI)
        {
           // Debug.Log(savedFill);
            ghostBarUI.DOFillAmount(savedFill, 0.01f);
            //ghostBarUI.fillAmount = savedFill;
            ghostBarUI.color = new Color(ghostBarUI.color.r, ghostBarUI.color.g, ghostBarUI.color.b, 1);
        }
        else
        {
            Debug.LogError(gameObject.name.ToString() + " IS MISSING ghostBarUI REFERENCE IN INSPECTOR");
        }

        //if (restrictedBarUI)
        //{
        //    restrictedBarUI.fillAmount = restrictedFill;
        //}
        //else
        //{
        //   // Debug.LogError(gameObject.name.ToString() + " IS MISSING restrictedBarUI REFERENCE IN INSPECTOR");
        //}
       // Debug.Log(gameObject.name + " INSTA- " + current + " - " + currentMax);

    }

    public void ResetBar(float p_current = 0, float p_currentMax = 1)
    {
        //Debug.Log("RESETTING: " + p_current + " " + p_currentMax);
        isResetting = true;
        isCurrentlyResettingCoroutine = true;
        current = p_current;
        currentMax = p_currentMax;
      

        float fill = p_current / p_currentMax;
        savedFill = fill;
        if (gameObject.activeSelf)
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
                runningCoroutine = null;
            }
            runningCoroutine = Co_UpdateBar(fill);
            StartCoroutine(runningCoroutine);
        }
        //Debug.Log(gameObject.name + " RESET- " + current + " - " + currentMax);
    }
   
    public void UpdateBar(float p_current = 0, float p_currentMax =1)
    {
        if (isCurrentlyResettingCoroutine == false)
        {
            //Debug.Log("UPDATING: " + p_current + " " + p_currentMax);
            current = p_current;
            currentMax = p_currentMax;

            float fill = current / currentMax;
            // Debug.Log("FILL: " + fill + " - " + current + " - " + currentMax);
           
            if (enabled)
            {
                if (runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                    runningCoroutine = null;
                }
            }

            if (gameObject.activeSelf)
            {
                StartCoroutine(Co_UpdateBar(fill));
            }

        }
       // Debug.Log(gameObject.name + " UPDA- " + current + " - " + currentMax);
    }
    IEnumerator Co_UpdateBar(float p_fill = 0)
    {
        //Debug.Log("CO_UPDATING: " + p_fill);
        if (!isResetting)
        {
           // Debug.Log(gameObject.name + " UPDATING BAR- " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);
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
                if (ghostBarFillTransition.amount <= 0)
                {
                    savedFill = p_fill;


                }
                else if (ghostBarFillTransition.amount > 0)
                {
                    if (ghostBarFillTransition.amount < -1)
                    {
                        savedFill = 0;
                    }
                    else
                    {
                        savedFill = ghostBarFillTransition.amount;
                    }
                
                }
                if (p_fill < -1)
                {

                    p_fill = 0;
                    
                }
                s.Join(ghostBarUI.DOFillAmount(p_fill, ghostBarFillTransition.transitionTime));
            
             //   Debug.Log("ghost bar shud be " + p_fill + " - "+ secondaryBarFillAmount.ToString());
             //   Debug.Log(gameObject.name + " UPDATING BAR- " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);
            }
   
            s.Play();
            yield return s.WaitForCompletion();
            ghostBarUI.color = defaultGhostBarColor;
          //  Debug.Log(gameObject.name + " UPODATIN- " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);

        }
        else
        {
            realBarUI.fillAmount = 1;
            //ghostBarUI.fillAmount = resetTransitionTime;
            yield return new WaitForSeconds(delayTime);
            realBarUI.fillAmount = 0;
            Tween defaultFill = ghostBarUI.DOFillAmount(0, resetTransitionTime);
            yield return defaultFill.WaitForCompletion();
            //Tween maxFill = realBarUI.DOFillAmount(1, 0.35f);
            //yield return maxFill.WaitForCompletion();

            //ghostBarUI.fillAmount = 0;
            //Tween defaultFill = realBarUI.DOFillAmount(0, resetTransitionTime);
            //yield return defaultFill.WaitForCompletion();

            isResetting = false;
          //  Debug.Log(gameObject.name + " older - " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);
            //p_fill -= 1;
           // Debug.Log(gameObject.name + " old- " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);
            //if (current - currentMax > 0)
            //{
            //    current -= currentMax;
            //}
            //if (p_fill > -1)
            //{
                if (runningCoroutine != null)
                {
                    StopCoroutine(runningCoroutine);
                    runningCoroutine = null;
                }
       


              // savedFill = p_fill;
              //  Debug.Log(gameObject.name + " UPDAINSIDING- " + current + " - " + currentMax + " - " + savedFill + " - " + p_fill);
              runningCoroutine = Co_UpdateBar(savedFill);
                StartCoroutine(runningCoroutine);
            isCurrentlyResettingCoroutine = false;
            //}
            // Debug.Log(gameObject.name + " UPDAOUTING- " + current + " - " + currentMax + " / " + savedFill);

            //StartCoroutine(Co_UpdateBar(p_fill));


        }
        
    }

}
