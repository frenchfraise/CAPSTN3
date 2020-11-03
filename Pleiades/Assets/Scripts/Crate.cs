using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject crate;
    public Sprite destroyed;

    public void Wreck()
    {
        crate.GetComponent<SpriteRenderer>().sprite = destroyed;
    }
}
