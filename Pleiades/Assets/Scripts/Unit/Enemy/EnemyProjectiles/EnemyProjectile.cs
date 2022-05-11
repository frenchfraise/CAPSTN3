using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public EnemyProjectileScriptableObject enemyProjectileScriptableObject;
    public float damage;
    public float speed;

    private Vector2 direction;

    private void Awake()
    {
        //enemyProjectileScriptableObject
    }

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        //target = new Vector2(player.position.x, player.position.y);
    }

    void Update()
    {
        //Shoot();
        //if (transform.position.x == target.x && transform.position.y == target.y)
        //{
        //    Destroy(this.gameObject);
        //}
    }

    public void Shoot()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    //    if (col.CompareTag("Player") && col.GetComponent<Gem>().blueGemIsActive == true)
    //    {
    //        col.GetComponent<Player>().TakeDamage(5);
    //        Destroy(this.gameObject);
    //    }
    //    else if (col.CompareTag("Player") && col.GetComponent<Gem>().redGemIsActive == true)
    //    {
    //        col.GetComponent<Player>().TakeDamage(10);
    //        Destroy(this.gameObject);
    //    }
    //    else if (col.CompareTag("Player") && col.GetComponent<Gem>().yellowGemIsActive == true)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else if (col.CompareTag("Player"))
    //    {
    //        col.GetComponent<Player>().TakeDamage(5);
    //        Destroy(this.gameObject);
    //    }
    }

}
