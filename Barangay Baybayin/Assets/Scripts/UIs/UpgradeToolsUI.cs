using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class UpgradeToolsUI : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanelUI;

    [NonReorderable] [SerializeField] public List<Sprite> plates = new List<Sprite>(); // test

    [SerializeField] private GameObject confirmPanelUI;
    [SerializeField] private TMP_Text currentToolNameText;
    [SerializeField] private Image currentToolImage;
    [SerializeField] private Image currentToolFrame;

    [SerializeField] private TMP_Text upgradePreviewToolNameText;
    [SerializeField] private Image upgradePreviewToolImage;
    [SerializeField] private Image upgradePreviewToolFrame;

    [SerializeField] public Sprite sufficientIcon;
    [SerializeField] public Sprite insufficientIcon;

    [SerializeField] public Sprite sufficientFrame;
    [SerializeField] public Sprite insufficientFrame;

    [SerializeField] private Image materialOneImage;
    [SerializeField] private Image materialOneIsValidImage;
    [SerializeField] private TMP_Text materialOneCurrentAmountText;
    [SerializeField] private TMP_Text materialOneMaxAmountText;

    [SerializeField] private Image materialTwoImage;
    [SerializeField] private Image materialTwoIsValidImage;
    [SerializeField] private TMP_Text materialTwoCurrentAmountText;
    [SerializeField] private TMP_Text materialTwoMaxAmountText;
    private int currentIndex;
    private bool materialOneRequirement = false;
    private bool materialTwoRequirement = false;
    private bool threeRequirement = false;
    private int matOneAmount;
    private SO_Item mato;
    private SO_Item matt;
    private int matTwoAmount;
    [SerializeField]
    private List<UpgradeToolUI> upgradeToolUIs = new List<UpgradeToolUI>();


    private void Awake()
    {
        foreach (UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.upgradeUI = this;
        }
       

    }

    private void OnEnable()
    {
        Panday.onPandaySpokenToEvent.AddListener(OpenButtonUIClicked);
    }

    private void OnDisable()
    {
        Panday.onPandaySpokenToEvent.RemoveListener(OpenButtonUIClicked);
    }
    public void ToolButtonUIClicked(int p_toolIndex)
    {
        materialOneRequirement = false;
        materialTwoRequirement = false;
        threeRequirement = false;
        currentIndex = p_toolIndex;
        confirmPanelUI.SetActive(true);
        selectionPanelUI.SetActive(false);
        UpdateCurrentToolUI(ToolManager.instance.tools[currentIndex]);
    }

    void UpdateCurrentToolUI(Tool p_currentTool)
    {
        SO_Tool currentSOTool = p_currentTool.so_Tool;
        int currentToolLevel = p_currentTool.craftLevel-1;
        int upgradeToolLevel = currentToolLevel+1;

        currentToolNameText.text = currentSOTool.name[p_currentTool.craftLevel - 1];
        currentToolImage.sprite = currentSOTool.equippedIcon[p_currentTool.craftLevel-1];
        currentToolFrame.sprite = plates[p_currentTool.craftLevel - 1];

        upgradePreviewToolNameText.text = currentSOTool.name[p_currentTool.craftLevel];
        upgradePreviewToolImage.sprite = currentSOTool.equippedIcon[p_currentTool.craftLevel];
        upgradePreviewToolFrame.sprite = plates[p_currentTool.craftLevel];

        CraftUpgradeItemRequirementsData craftUpgradeItemRequirementsData = currentSOTool.craftUpgradeItemRequirementsDatas[currentToolLevel];
        if (p_currentTool.proficiencyLevel >= craftUpgradeItemRequirementsData.requiredProficiencyLevel)
        {
            threeRequirement = true;
        }

        if (craftUpgradeItemRequirementsData.itemRequirements.Count > 0)
        {
            ItemUpgradeRequirement materialOne = craftUpgradeItemRequirementsData.itemRequirements[0];
            materialOneImage.sprite = materialOne.so_Item.icon;
            materialOneImage.gameObject.SetActive(true);
            mato = materialOne.so_Item;

            if (InventoryManager.GetItem(materialOne.so_Item).amount >= materialOne.requiredAmount)
            {
                materialOneIsValidImage.sprite = sufficientFrame;
                matOneAmount = materialOne.requiredAmount;
                materialOneRequirement = true;
            }
            else if (InventoryManager.GetItem(materialOne.so_Item).amount < materialOne.requiredAmount)
            {
                materialOneIsValidImage.sprite = insufficientFrame;
            }
            materialOneCurrentAmountText.text = InventoryManager.GetItem(materialOne.so_Item).amount.ToString();
            materialOneMaxAmountText.text = materialOne.requiredAmount.ToString();
            if (craftUpgradeItemRequirementsData.itemRequirements.Count > 1)
            {
                materialTwoImage.gameObject.SetActive(true);
                ItemUpgradeRequirement materialTwo = craftUpgradeItemRequirementsData.itemRequirements[1];
                materialTwoImage.sprite = materialTwo.so_Item.icon;
                matt = materialTwo.so_Item;
                if (InventoryManager.GetItem(materialTwo.so_Item).amount >= materialTwo.requiredAmount)
                {
                    materialTwoIsValidImage.sprite = sufficientFrame;
                    matTwoAmount = materialTwo.requiredAmount;
                    materialTwoRequirement = true;
                }
                else if (InventoryManager.GetItem(materialTwo.so_Item).amount < materialTwo.requiredAmount)
                {
                    materialTwoIsValidImage.sprite = insufficientFrame;
                }
                materialTwoCurrentAmountText.text = InventoryManager.GetItem(materialTwo.so_Item).amount.ToString();
                materialTwoMaxAmountText.text = materialTwo.requiredAmount.ToString();
            }
            else
            {
              
                materialTwoImage.gameObject.SetActive(false);
                materialTwoRequirement = true;
            }
        }
        else
        {
            materialOneImage.gameObject.SetActive(false);
            materialOneRequirement = true;
            

            materialTwoImage.gameObject.SetActive(false);
            materialTwoRequirement = true;
        }

    }


    public void ToolUpgradeButtonUIClicked()
    {
        
        if (ToolManager.instance.tools[currentIndex].craftLevel < ToolManager.instance.tools[currentIndex].so_Tool.maxCraftLevel)
        {
            //Debug.Log("TEST " + materialOneRequirement + " - " + materialTwoRequirement + " - "+ threeRequirement + " - " + ToolManager.instance.tools[currentIndex].craftLevel);
            if (materialOneRequirement && materialTwoRequirement && threeRequirement)
            {
                //Debug.Log("TEST 2");
                if (mato != null)
                {
                    
                    InventoryManager.ReduceItem(mato, matOneAmount);
                    
                }
                else
                {
                    InventoryManager.ReduceItem(matt, matTwoAmount);
        
                }
         
                ToolManager.instance.tools[currentIndex].craftLevel++;

                UpgradeToolUI currentlyUpgradedToolUI = upgradeToolUIs[currentIndex];
                currentlyUpgradedToolUI.UpdateUI();

                selectionPanelUI.SetActive(true);
                confirmPanelUI.SetActive(false);
            }
            
        }
     

    }

    public void BackButtonUIClicked()
    {
        
        foreach(UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.UpdateUI();
        }    
        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);

    }

    public void QuitButtonUIClicked()
    {
        confirmPanelUI.SetActive(false);
        selectionPanelUI.SetActive(false);
        UIManager.onGameplayModeChangedEvent.Invoke(false);
        TimeManager.onPauseGameTime.Invoke(false);
    }

    public void OpenButtonUIClicked()
    {
        TimeManager.onPauseGameTime.Invoke(true);
        foreach (UpgradeToolUI upgradeToolUI in upgradeToolUIs)
        {
            upgradeToolUI.UpdateUI();
        }
        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);
        
        UIManager.onGameplayModeChangedEvent.Invoke(true);
    }
}
