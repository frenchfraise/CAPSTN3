using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UISlotMachineCountEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text numberValueText;
    [SerializeField] private string numberFormat = "N0";
    [SerializeField] private int fpsCount = 30;
    [SerializeField] private float duration = 1f;

    private IEnumerator CountText(int p_newValue, int p_Value = 0)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / fpsCount);
        int previousValue = p_Value;
        int stepAmount;

        if (p_newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((p_newValue - previousValue) / (fpsCount * duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((p_newValue - previousValue) / (fpsCount * duration));

        }

        if (previousValue < p_newValue)
        {
            //Increasing Counter

            while (previousValue < p_newValue)
            {
                previousValue += stepAmount;
                if (previousValue > p_newValue)
                {
                    previousValue = p_newValue;
                }

                //Update Text to new Value
                numberValueText.text = previousValue.ToString(numberFormat);

                yield return wait;

            }

        }
        else if (previousValue > p_newValue)
        {
            //Decreasing Counter
            while (previousValue > p_newValue)
            {
                previousValue += stepAmount;
                if (previousValue < p_newValue)
                {
                    previousValue = p_newValue;
                }

                //Update Text to new Value
                numberValueText.text = previousValue.ToString(numberFormat);

                yield return wait;

            }

        }
    }
}
