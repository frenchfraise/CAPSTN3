﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyProjectile : EnemyProjectile
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && col.GetComponent<Gem>().blueGemIsActive == true)
        {
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("Player") && col.GetComponent<Gem>().redGemIsActive == true)
        {
            col.GetComponent<Player>().TakeDamage(5);
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("Player") && col.GetComponent<Gem>().yellowGemIsActive == true)
        {
            col.GetComponent<Player>().TakeDamage(10);
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().TakeDamage(5);
            Destroy(this.gameObject);
        }
    }
}
