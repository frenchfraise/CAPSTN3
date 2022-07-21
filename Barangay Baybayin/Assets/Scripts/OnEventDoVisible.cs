using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]

public class ActionVisibleUI
{
    [NonReorderable][SerializeField] public List<bool> actionPartVisible;
}
public class OnEventDoVisible : OnEventDoAction
{
    [SerializeField]
    private Image imageAffected;
    [NonReorderable][SerializeField] private List<ActionVisibleUI> actionVisible;
    public override void DoAction(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {
        base.DoAction(p_actionParameterAID, p_actionParameterBID);
        int AID = p_actionParameterAID;
        int BID = p_actionParameterBID;
        if (p_actionParameterAID == -1)
        {
            AID = 0;
        }
        if (p_actionParameterBID == -1)
        {
            BID = 0;
        }
        imageAffected.enabled = actionVisible[AID].actionPartVisible[BID];


    }
}
