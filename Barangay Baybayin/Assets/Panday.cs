using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panday : InteractibleObject
{
    [SerializeField]
    private int index;

    protected override void OnInteract()
    {
        UIManager.instance.upgradeUI.OpenButtonUIClicked();
    }
}
