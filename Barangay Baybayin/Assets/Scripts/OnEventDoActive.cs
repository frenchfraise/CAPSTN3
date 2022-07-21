using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ActionBool
{
    [NonReorderable][SerializeField] public List<bool> actionPartBool;
}
public class OnEventDoActive : OnEventDoAction
{
    [SerializeField]
    private GameObject gameObjectAffected;
    [NonReorderable][SerializeField] private List<ActionBool> actionBool;
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
        gameObjectAffected.SetActive(actionBool[AID].actionPartBool[BID]);


    }

    public override void DoPostAction(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {
       
        gameObjectAffected.SetActive(false);


    }
}
