using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class ResourceCategory
{
    [SerializeField] public string name;
    public List<Resource> resources = new List<Resource>();
}
