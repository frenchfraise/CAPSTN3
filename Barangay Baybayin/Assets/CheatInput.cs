using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatInput : MonoBehaviour
{
    [SerializeField] private KeyCode inputKey;
    [SerializeField] private CheatAction cheatAction;

    private void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            cheatAction.DoAction();
        }
    }
}
