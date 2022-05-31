using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DigitalClock : MonoBehaviour
{
    public TMP_Text display;
    public TimeManager timeManager;

    private void OnEnable()
    {
        timeManager.onHourChanged.AddListener(UpdateTime);
        timeManager.onMinuteChanged.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        timeManager.onHourChanged.RemoveListener(UpdateTime);
        timeManager.onMinuteChanged.RemoveListener(UpdateTime);
    }

    // Update is called once per frame
    private void UpdateTime()
    {        
        //display.SetText(TimeManager.instance.Clock12Hour());
        // display.text = timeManager.Clock12Hour();
        display.text = $"{TimeManager.hour:00}:{TimeManager.minute:00}";
    }
}
