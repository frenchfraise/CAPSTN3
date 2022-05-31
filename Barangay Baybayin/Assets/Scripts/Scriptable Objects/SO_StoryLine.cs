using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Story line Scriptable Object", menuName = "Scriptable Objects/Story Line")]
public class SO_StoryLine : ScriptableObject
{
    public List<SO_Questline> questLines = new List<SO_Questline>();
}
