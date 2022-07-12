using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventSubscriber : EventSubscriber
{

    protected override void CheckIDMatches(string p_eventID, int p_actionParameterAID, int p_actionParameterBID = -1)
    {
        if (id == p_eventID)
        {
            DoAllActions(p_actionParameterAID, p_actionParameterBID);
        }
    }
    protected override void DoAllActions(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

        for (int i = 0; i < onEventDoActions.Count; i++)
        {
            onEventDoActions[i].DoAction(p_actionParameterAID, p_actionParameterBID);
        }
    }
}
