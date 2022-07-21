using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ItemUI : MonoBehaviour
{
    public ItemData itemData;
    
    [SerializeField]
    public float targetTransitionTime;
    [SerializeField]
    public float defaultTransitionTime;

    [SerializeField]
    public RectTransform targetSizeTransition;
    [SerializeField]
    public RectTransform defaultSizeTransition;
    [SerializeField] public TMP_Text itemNameText;
    [SerializeField] public TMP_Text itemAmountText;
    [SerializeField] public Image itemIconImage;
    [SerializeField] public RectTransform amountTextRectTransform;
    [SerializeField] public RectTransform frameRectTransform;
    public void InitializeValues(string p_itemName, string p_itemAmount, Sprite p_itemIcon)
    {
      //  Debug.Log("INITIALIZED " + p_itemAmount);
        itemNameText.text = p_itemName;
        itemAmountText.text = p_itemAmount;
        itemIconImage.sprite = p_itemIcon;
        UIManager.onGameplayModeChangedEvent.AddListener(GameplayModeChangedEvent);
    }

    public void DeinitializeValues()
    {
  
        UIManager.onGameplayModeChangedEvent.RemoveListener(GameplayModeChangedEvent);
    }
    public void InstantUpdateText()
    {
       
        itemAmountText.text = (itemData.amount).ToString();
        Color32 color = new Color32(255, 255, 255, 255);
      
        itemAmountText.color = (color);
        if (defaultSizeTransition !=  null)
        {
            amountTextRectTransform.sizeDelta = defaultSizeTransition.sizeDelta;
            frameRectTransform.sizeDelta = defaultSizeTransition.sizeDelta;
        }


    }
    void GameplayModeChangedEvent(bool p_bool)
    {
        InstantUpdateText();
    }

    public IEnumerator Co_UpdateText(int p_newAmount)
    {
        itemAmountText.text = (itemData.amount + p_newAmount).ToString();
        Sequence sequence = DOTween.Sequence();
        Color32 color = new Color32(255, 0, 0, 255);
        if (p_newAmount+ itemData.amount > itemData.amount)
        {
            color = new Color32(0, 255, 0, 255);
        }
        itemData.amount += p_newAmount;
        sequence.Append(itemAmountText.DOColor(color, targetTransitionTime));
        sequence.Join(amountTextRectTransform.DOSizeDelta(targetSizeTransition.sizeDelta, targetTransitionTime, false));
        sequence.Join(frameRectTransform.DOSizeDelta(targetSizeTransition.sizeDelta, targetTransitionTime, false));
        //sequence.Join(amountText.DOFade(0, colorTransitionTime));
        sequence.Play();
        yield return sequence.WaitForCompletion();
        color = new Color32(255, 255, 255, 255);
        Sequence sequenceTwo = DOTween.Sequence();
        sequenceTwo.Append(itemAmountText.DOColor(color, defaultTransitionTime));
        sequenceTwo.Join(amountTextRectTransform.DOSizeDelta(defaultSizeTransition.sizeDelta, defaultTransitionTime, false));
        sequenceTwo.Join(frameRectTransform.DOSizeDelta(defaultSizeTransition.sizeDelta, defaultTransitionTime, false));
        //sequence.Join(amountText.DOFade(0, colorTransitionTime));
        sequenceTwo.Play();


    }
}
