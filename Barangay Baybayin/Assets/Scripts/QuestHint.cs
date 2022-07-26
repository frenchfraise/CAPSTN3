using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventSubscriber))]
public class QuestHint : MonoBehaviour
{
    WorldEventSubscriber worldEventSubscriber;
    public Animator anim;
 
    private void Awake()
    {
        if(TryGetComponent(out OnEventDoTransform eventDoTransform))
        {
            eventDoTransform.onAllActionsDoneEvent.AddListener(Subscriber);
        }
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
        worldEventSubscriber = GetComponent<WorldEventSubscriber>();
    }

    private void OnDestroy()
    {
        if (TryGetComponent(out OnEventDoTransform eventDoTransform))
        {
            eventDoTransform.onAllActionsDoneEvent.RemoveListener(Subscriber);
        }
        UIManager.onGameplayModeChangedEvent.RemoveListener(OnGameplayModeChangedEvent);
    }
    private void OnEnable()
    {


        //worldEventSubscriber.ForceEvents(0,0);
        OnEventDoTransform eventTrans = GetComponent<OnEventDoTransform>();
        transform.position = eventTrans.actionTransform[0].actionPartTransform[0].position;
        
    
    }

    private void OnGameplayModeChangedEvent(bool p_isActive)
    {
       // iconHoverEffect.gameObject.SetActive(!p_isActive);
        if (!p_isActive == true)
        {
            anim.enabled = true;
        }
        else if (!p_isActive == false)
        {
            anim.enabled = false;


        }
    }

    public void Subscriber()
    {
        
    }
 

}
