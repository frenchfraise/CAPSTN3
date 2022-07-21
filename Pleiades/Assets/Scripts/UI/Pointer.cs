using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    float originalY;
    public float floatStrength;

    void Awake()
    {
        floatStrength = 5;
    }

    void Start()
    {
        this.originalY = this.transform.position.y;
    }

    void Update()
    {
        Bob();
    }

    public void Bob()
    {
        transform.position = new Vector3(transform.position.x,
                                        originalY + ((float)Mathf.Sin(Time.time * 5f) * floatStrength),
                                        transform.position.z);
    }
}
