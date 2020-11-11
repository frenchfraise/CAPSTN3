using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactables
{
    public GameObject crate;
    public Sprite destroyed;
    public BoxCollider2D boxCol;
    public int indexNo;

    private void Start()
    {
        boxCol = this.GetComponent<BoxCollider2D>();
    }

    public virtual void Wreck()
    {
        crate.GetComponent<SpriteRenderer>().sprite = destroyed;
        interactedWith = true;

        //instance.CheckSet(indexNo);

        boxCol.enabled = false;
        PuzzleManager.instance.OnItemInteracted(indexNo);
    }
}
