using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Image healthBarUI;
    public void Start()
    {
        healthBarUI = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateUI(Health p_healthComponent)
    {
        healthBarUI.fillAmount = p_healthComponent.currentHealth / p_healthComponent.maxHealth;
    }
}
