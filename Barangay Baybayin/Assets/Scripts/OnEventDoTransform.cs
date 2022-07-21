using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ActionTransform
{
    [NonReorderable][SerializeField] public List<Transform> actionPartTransform;
}



public class OnEventDoTransform : OnEventDoAction
{
    [SerializeField]
    private Transform transformAffected;
    [NonReorderable][SerializeField] private List<ActionTransform> actionTransform;
    public override void DoAction(int p_actionParameterAID =-1, int p_actionParameterBID = -1)
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
        transformAffected.position = actionTransform[AID].actionPartTransform[BID].position;

        
    }
}
