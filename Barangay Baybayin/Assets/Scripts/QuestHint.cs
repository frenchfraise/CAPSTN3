using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventSubscriber))]
public class QuestHint : MonoBehaviour
{
    WorldEventSubscriber worldEventSubscriber;
    [SerializeField] private HoverEffect iconHoverEffect;
    [SerializeField] private RadiateScaleEffect radiateScaleEffect;
    private void OnEnable()
    {
        iconHoverEffect.startYPosition = iconHoverEffect.transform.position.y;
        worldEventSubscriber =GetComponent<WorldEventSubscriber>();
        iconHoverEffect.gameObject.SetActive(true);
        iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
        StartCoroutine(iconHoverEffect.runningCoroutine);
        radiateScaleEffect.runningCoroutine = radiateScaleEffect.Co_Scale();
        StartCoroutine(radiateScaleEffect.runningCoroutine);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
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


 

}
