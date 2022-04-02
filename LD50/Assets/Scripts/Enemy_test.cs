using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_test : Enemy
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }
}
