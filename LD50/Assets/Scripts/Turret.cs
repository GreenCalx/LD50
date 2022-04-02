using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Module
{
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
        /*Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < range)
            {
                enemy.Die();
            }
        }*/
    }
}
