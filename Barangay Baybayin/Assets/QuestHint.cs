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
        // worldEventSubscriber.onEventDoActionFinished.AddListener();
    }

    public void test()
    {
        //iconHoverEffect.startYPosition;

    }

}
