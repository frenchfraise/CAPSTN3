using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("RedEnemy"))
        {
            col.GetComponent<RedEnemy>().TakeDamage();   
        }
        else if (col.CompareTag("Torch"))
        {
            if (col.GetComponent<Torch>().interactedWith == false)
            {
                col.GetComponent<Torch>().LightThis();
            }
        }

        Destroy(this.gameObject);
    }
}
