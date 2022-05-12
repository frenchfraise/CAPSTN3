using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public ElementalTypeSO elementalType;
    public float currentHealth;
    public float maxHealth;
    public int damage;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float inSight;

    private float attackRate;
    public float startTimeBtwShots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
