using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventSubscriber : EventSubscriber
{
    protected override void Awake()
    {
        //base.Awake();
        TutorialManager.onTutorialEventEndedEvent.AddListener(TutorialEventEndedEvent);
        StorylineManager.onWorldEventEndedEvent.AddListener(DoAllPostActions);
    }
    void TutorialEventEndedEvent(int i)
    {
       // Debug.Log(gameObject.name + " p_eventID: " + i);
        DoAllActions(p_actionParameterBID: i);
    }

    //protected override void CheckIDMatches(string p_eventID, int p_actionParameterAID, int p_actionParameterBID = -1)
    //{
    //    if (p_eventID == "EQUIPPINGWRONGTOOL"|| 
    //        p_eventID == "SWINGINGWRONGTOOL" ||
    //        p_eventID == "RETURNTOCURRENTTUTORIAL" ||
    //        p_eventID == "O-0" ||
    //        p_eventID == "O-1" ||
    //        p_eventID == "O-2" ||
    //        p_eventID == "O-3" ||
    //        p_eventID == "O-4" ||
    //        p_eventID == "O-5" ||
    //        p_eventID == "O-6" ||
    //        p_eventID == "O-7" ||
    //        p_eventID == "O-8" ||
    //        p_eventID == "O-9" ||
    //        p_eventID == "Q-P"
    //        )
    //    {
    //        Debug.Log(gameObject.name + " p_eventID: " + p_eventID);
    //        DoAllActions(p_actionParameterAID, TutorialManager.instance.currentIndex);
    //    }
    //}
    protected override void DoAllActions(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

        for (int i = 0; i < onEventDoActions.Count; i++)
        {
            onEventDoActions[i].DoAction(p_actionParameterAID, p_actionParameterBID);
        }
    }
    protected override void DoAllPostActions(string p_eventID, int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

        for (int i = 0; i < onEventDoActions.Count; i++)
        {
            onEventDoActions[i].DoPostAction(p_actionParameterAID, p_actionParameterBID);
        }
    }
}
