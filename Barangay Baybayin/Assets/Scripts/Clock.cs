using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public TimeManager timeManager;
    public Image nightBackground;
    public RectTransform hand;

    const float hoursToDegrees = 180 / 24;

    // Start is called before the first frame update
    void Start()
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90);
        nightBackground.fillAmount = timeManager.realSecondsPerNight / 2;
    }

    // Update is called once per frame
    void Update()
    {
        hand.localRotation = Quaternion.Euler(0, 0, 90 + hoursToDegrees * ((timeManager.GetHour() + TimeManager.hoursInDay - timeManager.sunriseHour) % TimeManager.hoursInDay));
    }
}
