using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSwitch : MonoBehaviour
{
    public void EnableMovement()
    {
        PlayerManager.instance.playerMovement.isMoving = true;
    }
    public void DisableMovement()
    {
        PlayerManager.instance.playerMovement.isMoving = false;
    }
}
