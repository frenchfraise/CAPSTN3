using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    public void OnToolSwing()
    {
        AudioManager.instance.GetSoundByName("Swing").source.Play();
    }
    public void EnableMovement()
    {
        PlayerManager.instance.playerMovement.isMoving = true;
    }
    public void DisableMovement()
    {
        PlayerManager.instance.playerMovement.isMoving = false;
    }
}
