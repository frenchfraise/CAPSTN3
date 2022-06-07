using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolChargesUI : MonoBehaviour
{
    private TMP_Text toolChargeTMP;
    private float current;
    private float max;

    private void Awake()
    {
        toolChargeTMP = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        ToolManager.onSpecialPointsModifiedEvent.AddListener(UpdateCharges);
        ToolManager.onSpecialPointsFilledEvent.AddListener(FillCharges);
    }

    private void OnDisable()
    {
        ToolManager.onSpecialPointsModifiedEvent.RemoveListener(UpdateCharges);
        ToolManager.onSpecialPointsFilledEvent.RemoveListener(FillCharges);
    }

    void UpdateCharges(float p_current, float p_max)
    {
        /*float chargeCount;
        if (p_current % p_max == p_current)
        {
            // Debug.Log("Beep. Boop.");
            chargeCount = p_current / p_max;
            toolChargeTMP.text = $"{chargeCount} / {p_max}";
        }*/
        current = p_current;
        max = p_max;
        toolChargeTMP.text = $"{p_current} / {p_max}";
    }

    void FillCharges()
    {
        toolChargeTMP.text = $"100 / {max}";
    }
}
