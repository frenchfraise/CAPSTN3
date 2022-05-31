using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : InteractibleObject
{
    public SO_Item p_item;
    protected override void OnInteract()
    {
        UIManager.instance.recipeUpgrade.SetActive(true);
    }

    public void Test(int p_)
    {
        ToolManager.instance.tools[p_].CheckUpgradeCraftLevel(p_item);
    }
}
