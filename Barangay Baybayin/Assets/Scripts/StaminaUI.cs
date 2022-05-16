using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image staminaBarUI;
    [SerializeField] private Image delayedBar;
    float fill;
    private void Awake()
    {
        //staminaBarUI = transform.GetChild(0).GetComponent<Image>();
    }
    public void UpdateUI(Stamina p_staminaComponent)
    {
        fill = p_staminaComponent.currentStamina / p_staminaComponent.maxStamina;
        StartCoroutine(Co_Test());
    }

    IEnumerator Co_Test()
    {
        //white
        staminaBarUI.color = new Color32(255, 255, 255,255);

        //black
        Tween WhiteToBlack = staminaBarUI.DOColor(new Color(0, 0, 0), 0.05f);
        yield return WhiteToBlack.WaitForCompletion();

        Tween BlackToRed = staminaBarUI.DOColor(new Color(255, 0, 0), 0.05f);
        yield return BlackToRed.WaitForCompletion();

        staminaBarUI.color = new Color32(250, 186, 20,255);//reset
        staminaBarUI.fillAmount = fill;
        yield return new WaitForSeconds(1f);

        Sequence s = DOTween.Sequence();
        s.Join(delayedBar.DOFade(0f, 0.35f));
        s.Join(delayedBar.DOFillAmount(fill, 0.35f));
        s.Play();
        yield return s.WaitForCompletion();

        delayedBar.color = new Color32(250, 255, 255, 255);//reset

    }
}
