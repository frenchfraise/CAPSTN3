using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGem : MonoBehaviour
{
    int value = 20;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            col.GetComponent<Player>().AddHealth(20);
            Destroy(this.gameObject);
        }
    }
}
