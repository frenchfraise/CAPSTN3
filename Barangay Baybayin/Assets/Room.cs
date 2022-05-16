using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceNodeDrop
{
    public ResourceNode resourceNode;
    public float chance;

}

public class Room : MonoBehaviour
{
    public string roomName;
    public List<ResourceNodeDrop> availableResourceNodeDrops = new List<ResourceNodeDrop>(); // populate by node

}
