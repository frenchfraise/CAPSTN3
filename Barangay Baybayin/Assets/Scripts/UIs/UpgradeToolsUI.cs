using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeToolsUI : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanelUI;

    [SerializeField] private GameObject confirmPanelUI;
    [SerializeField] private TMP_Text currentToolNameText;
    [SerializeField] private Image currentToolImage;

    [SerializeField] private TMP_Text upgradePreviewToolNameText;
    [SerializeField] private Image upgradePreviewToolImage;

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
    [SerializeField]
    private List<UpgradeToolUI> upgradeToolUIs = new List<UpgradeToolUI>();

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
        int currentToolLevel = p_currentTool.craftLevel;
        int upgradeToolLevel = currentToolLevel+1;


        currentToolNameText.text = currentSOTool.name;
        currentToolImage.sprite = currentSOTool.equippedIcon;

        upgradePreviewToolNameText.text = currentSOTool.name;
        upgradePreviewToolImage.sprite = currentSOTool.equippedIcon;
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


         
            if (InventoryManager.GetItem(materialOne.so_Item).amount >= materialOne.requiredAmount)
            {
                materialOneIsValidImage.color = new Color32(0, 255, 0, 225);
                materialOneRequirement = true;
            }
            else if (InventoryManager.GetItem(materialOne.so_Item).amount < materialOne.requiredAmount)
            {
                materialOneIsValidImage.color = new Color32(255,0 , 0, 225);
            }
            materialOneCurrentAmountText.text = InventoryManager.GetItem(materialOne.so_Item).amount.ToString();
            materialOneMaxAmountText.text = materialOne.requiredAmount.ToString();
            if (craftUpgradeItemRequirementsData.itemRequirements.Count > 1)
            {
                materialTwoImage.gameObject.SetActive(true);
                ItemUpgradeRequirement materialTwo = craftUpgradeItemRequirementsData.itemRequirements[1];
                materialTwoImage.sprite = materialTwo.so_Item.icon;

                if (InventoryManager.GetItem(materialTwo.so_Item).amount >= materialTwo.requiredAmount)
                {
                    materialTwoIsValidImage.color = new Color32(0, 255, 0, 225);
                    materialTwoRequirement = true;
                }
                else if (InventoryManager.GetItem(materialTwo.so_Item).amount < materialTwo.requiredAmount)
                {
                    materialTwoIsValidImage.color = new Color32(255, 0, 0, 225);
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
            Debug.Log("TEST " + materialOneRequirement + " - " + materialTwoRequirement + " - "+ threeRequirement + " - " + ToolManager.instance.tools[currentIndex].craftLevel);
            if (materialOneRequirement && materialTwoRequirement && threeRequirement)
            {
                Debug.Log("TEST 2");
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
        
        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);

    }

    public void QuitButtonUIClicked()
    {

        gameObject.SetActive(false);

    }

    public void OpenButtonUIClicked()
    {

        selectionPanelUI.SetActive(true);
        confirmPanelUI.SetActive(false);
        gameObject.SetActive(true);

    }
}
