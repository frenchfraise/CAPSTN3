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
        iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
        StartCoroutine(iconHoverEffect.runningCoroutine);
        UIManager.onGameplayModeChangedEvent.AddListener(OnGameplayModeChangedEvent);
    }

    private void OnGameplayModeChangedEvent(bool p_isActive)
    {
        iconHoverEffect.gameObject.SetActive(!p_isActive);
        if (!p_isActive == true)
        {
            iconHoverEffect.runningCoroutine = iconHoverEffect.Co_Hover();
            iconHoverEffect.gameObject.SetActive(true);
            StartCoroutine(iconHoverEffect.runningCoroutine);
        }
        else if (!p_isActive == false)
        {
            if (iconHoverEffect.runningCoroutine != null)
            {
                iconHoverEffect.gameObject.SetActive(false);
                StopCoroutine(iconHoverEffect.runningCoroutine);
                iconHoverEffect.runningCoroutine = null;
            }
          
        }
    }


 

}
