using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundCategory
{
    public string name;
    [NonReorderable] public List<Sound> sound;
}
