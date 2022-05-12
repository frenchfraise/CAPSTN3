using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaUI : MonoBehaviour
{
    private Image staminaBarUI;

    private void Awake()
    {
        staminaBarUI = transform.GetChild(0).GetComponent<Image>();
    }
    public void UpdateUI(Stamina p_staminaComponent)
    {
        staminaBarUI.fillAmount = p_staminaComponent.currentStamina / p_staminaComponent.maxStamina;
    }
}
