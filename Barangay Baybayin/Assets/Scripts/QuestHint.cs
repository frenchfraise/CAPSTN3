using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorldEventSubscriber))]
public class QuestHint : MonoBehaviour
{
    WorldEventSubscriber worldEventSubscriber;
    [SerializeField] private HoverEffect iconHoverEffect;
    private void OnEnable()
    {
        iconHoverEffect.startYPosition = iconHoverEffect.transform.position.y;
        worldEventSubscriber =GetComponent<WorldEventSubscriber>();
        iconHoverEffect.gameObject.SetActive(true);
        iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
        StartCoroutine(iconHoverEffect.runningCoroutine);
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

            iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
            StartCoroutine(iconHoverEffect.runningCoroutine);
        }
        else if (!p_isActive == false)
        {
            if (iconHoverEffect.runningCoroutine != null)
            {
                StopCoroutine(iconHoverEffect.runningCoroutine);
                iconHoverEffect.runningCoroutine = null;
                iconHoverEffect.gameObject.SetActive(false);
      
            }
          
        }
    }


 

}
