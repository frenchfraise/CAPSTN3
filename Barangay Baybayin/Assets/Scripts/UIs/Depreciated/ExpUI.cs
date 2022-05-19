using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ExpUI : MonoBehaviour
{
    [SerializeField] private Image barUI;
    [SerializeField] private Image delayedBar;
    float fill;
    private void Awake()
    {
        //staminaBarUI = transform.GetChild(0).GetComponent<Image>();
    }
    public void InstantUpdateUI(float p_current, float p_max)
    {
        StopAllCoroutines();
        fill = p_current / p_max;
        delayedBar.fillAmount = fill;
        barUI.fillAmount = fill;
    }

    public void UpdateUI(float p_current, float p_max)
    {
        fill = p_current / p_max;
        StartCoroutine(Co_Test());
    }

    public void LevelUpdateUI(float p_current, float p_max)
    {
        fill = p_current / p_max;
        StartCoroutine(Co_Test2());
    }

    IEnumerator Co_Test()
    {
      
        delayedBar.fillAmount = fill;
        yield return new WaitForSeconds(1f);

        Tween test = barUI.DOFillAmount(fill, 0.35f);
        yield return test.WaitForCompletion();

    }

    IEnumerator Co_Test2()
    {

        delayedBar.fillAmount = 1;
        yield return new WaitForSeconds(1f);

        Tween test = barUI.DOFillAmount(1, 0.35f);
        yield return test.WaitForCompletion();

        delayedBar.fillAmount = 0;
        Tween testt = barUI.DOFillAmount(0, 0.15f);
        yield return testt.WaitForCompletion();

        StartCoroutine(Co_Test());


    }
}
