using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TutorialPanelUI : MonoBehaviour
{
    public TMP_Text titleText;
    public Image demoImage;
    public TMP_Text descriptionText;
    public GameObject nextButton;
    public GameObject backButton;
    public SO_TutorialPanelUI currentSO_TutPanels;    
    public int currentPanelIndex;

    public void NextButtonHit()
    {
        if (currentPanelIndex >= 0) backButton.SetActive(true);
        if (currentPanelIndex < currentSO_TutPanels.panels.Count - 1)
        {
            currentPanelIndex++;
            TPUI currentPanel = currentSO_TutPanels.panels[currentPanelIndex];
            titleText.text = currentPanel.tutorialTitle;
            //demoImage.sprite = currentPanel.image;
            descriptionText.text = currentPanel.words;
        }
        if (currentPanelIndex >= currentSO_TutPanels.panels.Count - 1)
        {
            nextButton.SetActive(false);
        }
    }

    public void BackButtonHit()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            nextButton.SetActive(true);
            TPUI currentPanel = currentSO_TutPanels.panels[currentPanelIndex];
            titleText.text = currentPanel.tutorialTitle;
            //demoImage.sprite = currentPanel.image;
            descriptionText.text = currentPanel.words;
        }
        if (currentPanelIndex <= 0)
        {
            backButton.SetActive(false);
        }
    }
}
