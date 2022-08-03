using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDoActionFinished : UnityEvent { };
public class EventSubscriber : MonoBehaviour
{
    [NonReorderable]
    [SerializeField]
    protected List<OnEventDoAction> onEventDoActions;

    [SerializeField]
    protected string id;

    protected EventDoActionFinished onEventDoActionFinished = new EventDoActionFinished();

    protected virtual void Awake()
    {
        StorylineManager.onWorldEventEndedEvent.AddListener(CheckIDMatches);
    }

    protected virtual void OnDestroy()
    {
        StorylineManager.onWorldEventEndedEvent.RemoveListener(CheckIDMatches);
    }
 

    protected virtual void CheckIDMatches(string p_eventID, int p_actionParameterAID, int p_actionParameterBID = -1)
    {
      
    }
    protected virtual void DoAllActions(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {
    }
    protected virtual void DoAllPostActions(string p_eventID, int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {
    }
}
