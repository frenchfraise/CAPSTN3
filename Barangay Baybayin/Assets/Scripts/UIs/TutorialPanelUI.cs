using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class TutorialPanelUI : MonoBehaviour
{
    public GameObject frame;
    public GameObject overheadUI;
    public TMP_Text overheadText;
    public TMP_Text titleText;
    public Image demoImage;
    public TMP_Text descriptionText;
    public GameObject nextButton;
    public GameObject backButton;
    //public SO_TutorialPanelUI currentSO_TutPanels;
    public int tutorialPanelPagesUIDataIndex;
    public int tutorialPanelUIDataIndex;
    [NonReorderable] public List<TutorialPanelUIData> tutorialPanelPagesUIData = new List<TutorialPanelUIData>();
    public bool specificToPage = false;
 
    //public List<Sprite> tutorialPages = new List<Sprite>();
    private void Awake()
    {


    }

    private void OnDestroy()
    {
   
    }

    public void RemindTutorialEvent(int p_tutorialPanelPagesUIDataIndex)
    {
        UIManager.onGameplayModeChangedEvent.Invoke(true);
        specificToPage = true;
        tutorialPanelPagesUIDataIndex = p_tutorialPanelPagesUIDataIndex;
        tutorialPanelUIDataIndex = 0;
        backButton.SetActive(false);
        if (tutorialPanelUIDataIndex < tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels.Count - 1)
        {
            nextButton.SetActive(true);

        }
       
      
        InitializeValues(0);
        frame.SetActive(true);
    }
    public void Open()
    {
     
        specificToPage = false;
        tutorialPanelUIDataIndex = 0;
        tutorialPanelPagesUIDataIndex = 0;
        backButton.SetActive(false);
        if (tutorialPanelUIDataIndex < tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels.Count - 1)
        {
            nextButton.SetActive(true);

        }
        else
        {
            if (tutorialPanelPagesUIDataIndex < tutorialPanelPagesUIData.Count - 1)
            {
                nextButton.SetActive(true);
            }
        }
        InitializeValues(0);

        frame.SetActive(true);

    }

    public void InitializeValues(int p_index)
    {
        TPUI currentPanel = tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels[p_index];
        titleText.text = currentPanel.tutorialTitle;
        demoImage.sprite = currentPanel.image;
        descriptionText.text = currentPanel.words;

    }
    public void NextButtonHit()
    {
       
        if (tutorialPanelUIDataIndex < tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels.Count - 1)
        {

            tutorialPanelUIDataIndex++;
            InitializeValues(tutorialPanelUIDataIndex);
            if (tutorialPanelUIDataIndex >= tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels.Count - 1)
            {
                if (specificToPage)
                {
             
                    nextButton.SetActive(false);
                }

            }
            if (tutorialPanelUIDataIndex > 0)
            {
                backButton.SetActive(true);
            }
        }
        
        if (!specificToPage)
        {
            if (tutorialPanelPagesUIDataIndex < tutorialPanelPagesUIData.Count - 1)
            {
                tutorialPanelPagesUIDataIndex++;
                tutorialPanelUIDataIndex = 0;
                InitializeValues(tutorialPanelUIDataIndex);
                if (tutorialPanelPagesUIDataIndex > 0)
                {
                    backButton.SetActive(true);
                }
            }
            if (tutorialPanelPagesUIDataIndex >= tutorialPanelPagesUIData.Count -1)
            {

                nextButton.SetActive(false);
            }

        }
        


    }

    public void BackButtonHit()
    {
        Debug.Log("TEST");

        if (tutorialPanelUIDataIndex > 0)
        {

            tutorialPanelUIDataIndex--;
            InitializeValues(tutorialPanelUIDataIndex);
            if (tutorialPanelUIDataIndex <= 0)
            {
                if (specificToPage)
                {
                
                    backButton.SetActive(false);
                }

            }
            if (tutorialPanelUIDataIndex < tutorialPanelPagesUIData[tutorialPanelPagesUIDataIndex].panels.Count - 1)
            {
                nextButton.SetActive(true);
            }
        }
        
        if (!specificToPage)
        {
            if (tutorialPanelPagesUIDataIndex > 0)
            {
                tutorialPanelPagesUIDataIndex--;
                tutorialPanelUIDataIndex = 0;
                InitializeValues(tutorialPanelUIDataIndex);
                if (tutorialPanelPagesUIDataIndex < tutorialPanelPagesUIData.Count - 1)
                {
                    nextButton.SetActive(true);
                }
            }
            if (tutorialPanelPagesUIDataIndex <= 0)
            {
                backButton.SetActive(false);
            }

        }
    

    }
}
