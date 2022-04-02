using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{

    public float explosion_duration = 2f;
    private double start_time;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Time.time - start_time) > explosion_duration )
        {
            Destroy(this.gameObject);
        }
    }
}
