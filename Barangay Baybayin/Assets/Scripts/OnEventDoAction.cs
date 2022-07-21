using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnEventDoAction : MonoBehaviour
{
    public virtual void DoAction(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

    }

    public virtual void DoPostAction(int p_actionParameterAID = -1, int p_actionParameterBID = -1)
    {

    }
}
