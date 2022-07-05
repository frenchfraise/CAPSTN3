using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public GameObject frame;
    public Button button;
    public GameObject overheadUI;
    public TMP_Text overheadText;
    public void SetVisibility(bool p_bool)
    {
        frame.SetActive(p_bool);
    }
    public void SetButton(bool p_bool)
    {
        button.enabled = (p_bool);
    }
    public void SetOverheadText(string p_string)
    {
        overheadText.text = p_string;
    }
}
