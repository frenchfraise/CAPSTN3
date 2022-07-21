using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public EdgeCollider2D edgeCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StorylineManager.onWorldEventEndedEvent.Invoke("CANTGOTHERE", 0, 0);
    }
}
