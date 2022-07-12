using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class ActionRectTransform
{
    [NonReorderable][SerializeField] public List<RectTransform> actionPartTransform;
}
public class OnEventDoAnchoredTransform : OnEventDoAction
{
    [SerializeField]
    private RectTransform transformAffected;
    [NonReorderable][SerializeField] private List<ActionRectTransform> actionTransform;
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
        transformAffected.anchoredPosition = actionTransform[AID].actionPartTransform[BID].anchoredPosition;


    }
}
