using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "lightingSchedule_", menuName = "Scriptable Objects/Lighting Schedule")]
public class SO_LightingSchedule : ScriptableObject
{
    public LightingBrightness[] lightingBrightnessArray;
}

[System.Serializable]
public struct LightingBrightness
{    
    public string weatherName;
    public int hour;
    public Color color;
    public float lightIntensity;
}
