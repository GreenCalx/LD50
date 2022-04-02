using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :  Observed
{
    public bool is_locked_by_player;
    public double lock_on_start_time;
    public double lock_on_elapsed_time;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init()
    {
        obs = GetComponentInParent<EnemyManager>();
        is_locked_by_player = false;
    }

    public void Die()
    {
        notify();
        Destroy(this.gameObject);
    }
}
