using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProficiencyCheat : CheatAction
{
    public override void DoAction()
    {
        GetComponent<ToolCaster>().current_Tool.ModifyProficiencyAmount(10);
      
    }
}
