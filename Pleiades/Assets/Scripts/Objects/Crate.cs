using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactables
{
    public GameObject crate;
    public Sprite destroyed;
    public BoxCollider2D boxCol;
    public int indexNo;

    Transform spawnPoint;
    public GameObject healthGem;

    private void Start()
    {
        boxCol = this.GetComponent<BoxCollider2D>();
        spawnPoint = this.GetComponent<Transform>();
    }

    public virtual void Wreck()
    {
        crate.GetComponent<SpriteRenderer>().sprite = destroyed;
        interactedWith = true;
        HealthChance();

        //instance.CheckSet(indexNo);

        boxCol.enabled = false;
        PuzzleManager.instance.OnItemInteracted(indexNo);
    }

    public void HealthChance()
    {
        int chance = Random.Range(0, 100);
        Debug.Log("chance: " + chance);
        if (chance <= 50)
        {
            Instantiate(healthGem, spawnPoint.position, Quaternion.identity);
        }
    }
}
