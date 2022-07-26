using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClockUI : MonoBehaviour
{
    [SerializeField] private RectTransform hand;

    const float hoursToDegrees = 320 / 24;

    // Start is called before the first frame update
    void Start()
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90); 
    }

    private void OnEnable()
    {
        TimeManager.onTimeChangedEvent.AddListener(UpdateTime);
    }

    private void OnDisable()
    {
        TimeManager.onTimeChangedEvent.RemoveListener(UpdateTime);
    }

    // Update is called once per frame
    private void UpdateTime(int hour24, int hour12, int minute, int minuteByTwos)
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90 + hoursToDegrees * ((hour24 + TimeManager.hoursInDay - TimeManager.sunriseHour) % TimeManager.hoursInDay));
    }
}
