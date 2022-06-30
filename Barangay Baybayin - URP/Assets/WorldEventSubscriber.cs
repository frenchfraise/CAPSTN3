using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDoActionFinished : UnityEvent { };
public class WorldEventSubscriber : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    private List<OnEventDoAction> onEventDoActions;

    [SerializeField]
    private string id;

    public EventDoActionFinished onEventDoActionFinished = new EventDoActionFinished();
    private void OnEnable()
    {
        StorylineManager.onWorldEvent.AddListener(CheckIDMatches);
    }

    private void CheckIDMatches(string p_eventID, int p_actionParameterAID, int p_actionParameterBID = -1)
    {
        if (id == p_eventID)
        {
            DoAllActions(p_actionParameterAID, p_actionParameterBID);
        }
    }
    private void DoAllActions(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

        for (int i = 0; i < onEventDoActions.Count; i++)
        {
            onEventDoActions[i].DoAction(p_actionParameterAID, p_actionParameterBID);
        }
    }
}
