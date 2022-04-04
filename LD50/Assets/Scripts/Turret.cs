using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Module
{
    public float shoot_time;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
        
        switch (level)
        {
            case 0:
                break;

            case 1:
                shoot_time = 1f;
                break;

            case 2:
                shoot_time = 0.3f;
                break;
        }
    }
}
