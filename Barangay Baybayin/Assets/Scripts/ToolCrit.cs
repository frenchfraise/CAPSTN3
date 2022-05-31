using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ToolCrit : MonoBehaviour

{
//    [SerializeField] private ToolCritMeterUI toolCritMeterUI;
//    public float currentCritGauge;
//    public float maxCritGauge = 10f;

//    public OnSpecialPointsUpdated OnCritMeterIncrease = new OnSpecialPointsUpdated();
//    public CritMeterFull OnCritMeterFull = new CritMeterFull();
//    public CritMeterEmpty OnCritMeterEmpty = new CritMeterEmpty();

//    private void OnEnable()
//    {
//        OnCritMeterIncrease.AddListener(toolCritMeterUI.UpdateBar);
//        OnCritMeterFull.AddListener(GetComponent<ToolCaster>().OnCriticalMeterFilled);
//        OnCritMeterEmpty.AddListener(GetComponent<ToolCaster>().OnCriticalMeterEmpty);
//    }

//    private void OnDisable()
//    {
//        OnCritMeterIncrease.RemoveListener(toolCritMeterUI.UpdateBar);
//        OnCritMeterFull.RemoveListener(GetComponent<ToolCaster>().OnCriticalMeterFilled);
//        OnCritMeterEmpty.RemoveListener(GetComponent<ToolCaster>().OnCriticalMeterEmpty);
//    }

//    public void CritMeterIncreased(float p_amount)
//    {
//        Debug.Log("crit increased: " + p_amount);
//        currentCritGauge += p_amount;
//        OnCritMeterIncrease.Invoke(currentCritGauge, maxCritGauge);
//        CheckCrit();
//    }

//    public void CheckCrit()
//    {
//        if (currentCritGauge >= maxCritGauge)
//        {
//            Debug.Log("Crit is full");
//            OnCritMeterFull.Invoke();            
//        }
//    }

//    public void CritMeterEmpty()
//    {
//        Debug.Log("Crit is empty");
//        currentCritGauge = 0;
//        toolCritMeterUI.ResetBar();
//        OnCritMeterEmpty.Invoke();
//    }
}
