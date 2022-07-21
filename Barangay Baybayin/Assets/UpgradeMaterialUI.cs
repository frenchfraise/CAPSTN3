using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeMaterialUI : MonoBehaviour
{
    public bool isInUse = false;
    public bool requirementFulfilled = false;
    [SerializeField] public Image iconImage;
    [SerializeField] public Image isValidImage;
    [SerializeField] public TMP_Text currentAmountText;
    [SerializeField] public TMP_Text maxAmountText;
    public int amountRequired;
    public SO_Item so_item;

}
