using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Module
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        GetComponentInChildren<TurretWeapon>().gameObject.SetActive(false);
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
                //GetComponentInChildren<PlayerWeapon>().gameObject.SetActive(true);
                break;
        }
    }
}
