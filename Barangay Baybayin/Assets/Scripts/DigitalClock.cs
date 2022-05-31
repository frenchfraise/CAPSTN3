using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DigitalClock : MonoBehaviour
{
    public TimeManager timeManager;
    TMP_Text display;

    private void Start()
    {
        display = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {        
        //display.SetText(TimeManager.instance.Clock12Hour());
        display.text = TimeManager.instance.Clock12Hour();
    }
}
