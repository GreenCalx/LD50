using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Observer
{
    public VillageManager VM;
    private List<EnemySpawner> spawners;
    public float spawn_interval = 5f;
    private float last_spawn_time;

    // Start is called before the first frame update
    void Start()
    {
        last_spawn_time = 0;
        refreshSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Time.time - last_spawn_time) > spawn_interval )
            spawn();
    }

    public void refreshSpawners()
    {
        spawners = new List<EnemySpawner>(GetComponentsInChildren<EnemySpawner>());
    }

    public void spawn()
    {
        last_spawn_time = Time.time;
        int spawn_index = Random.Range(0, spawners.Count-1);
        spawners[spawn_index].spawn( VM.getRandomHouse().transform );
    }

    public virtual void notifed(GameObject iGO)
    {
        base.notified(iGO);
    }
}
 