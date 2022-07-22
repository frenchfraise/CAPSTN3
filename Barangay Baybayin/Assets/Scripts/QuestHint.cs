using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventSubscriber))]
public class QuestHint : MonoBehaviour
{
    WorldEventSubscriber worldEventSubscriber;
    [SerializeField] private HoverEffect iconHoverEffect;
    [SerializeField] private RadiateScaleEffect radiateScaleEffect;
 
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
        iconHoverEffect.startYPosition = iconHoverEffect.transform.position.y;
        iconHoverEffect.gameObject.SetActive(true);
        iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
        StartCoroutine(iconHoverEffect.runningCoroutine);
        radiateScaleEffect.runningCoroutine = radiateScaleEffect.Co_Scale();
        StartCoroutine(radiateScaleEffect.runningCoroutine);
    
    }

    private void OnGameplayModeChangedEvent(bool p_isActive)
    {
       // iconHoverEffect.gameObject.SetActive(!p_isActive);
        if (!p_isActive == true)
        {
            iconHoverEffect.gameObject.SetActive(true);
            if (iconHoverEffect.runningCoroutine != null)
            {
                StopCoroutine(iconHoverEffect.runningCoroutine);
                iconHoverEffect.runningCoroutine = null;
            }
            iconHoverEffect.sr.enabled = true;
            iconHoverEffect.srIcon.enabled = true;
            iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
            StartCoroutine(iconHoverEffect.runningCoroutine);

            if (radiateScaleEffect.runningCoroutine != null)
            {
                StopCoroutine(radiateScaleEffect.runningCoroutine);
                radiateScaleEffect.runningCoroutine = null;
            }
            radiateScaleEffect.runningCoroutine = radiateScaleEffect.Co_Scale();
            StartCoroutine(radiateScaleEffect.runningCoroutine);
        }
        else if (!p_isActive == false)
        {
            if (iconHoverEffect.runningCoroutine != null)
            {
                StopCoroutine(iconHoverEffect.runningCoroutine);
                iconHoverEffect.runningCoroutine = null;
                iconHoverEffect.srIcon.enabled = false;
                iconHoverEffect.sr.enabled = false;//gameObject.SetActive(false);
      
            }
            else if (radiateScaleEffect.runningCoroutine != null)
            {
                StopCoroutine(radiateScaleEffect.runningCoroutine);
                radiateScaleEffect.runningCoroutine = null;
        

            }

        }
    }

    public void Subscriber()
    {
        iconHoverEffect.startYPosition = iconHoverEffect.transform.position.y;
    }
 

}
