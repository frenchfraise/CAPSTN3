using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePointer : MonoBehaviour
{
    float originalX;
    public float floatStrength;

    void Awake()
    {
        floatStrength = 5;
    }

    void Start()
    {
        this.originalX = this.transform.position.x;
    }

    void Update()
    {
        Bob();
    }

    public void Bob()
    {
        transform.position = new Vector3(originalX + ((float)Mathf.Sin(Time.time * 5f) * floatStrength),
                                         transform.position.y,
                                         transform.position.z);
    }


}
