using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Observer
{
    public VillageManager VM;
    private List<EnemySpawner> spawners;
    public float spawn_interval = 5f;
    public float spawn_interval_decrease_rate = 1f;
    private float last_spawn_time;
    public Transform evacPoint;

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

        spawn_interval *= (1 - spawn_interval_decrease_rate * Time.deltaTime);
    }

    public void refreshSpawners()
    {
        spawners = new List<EnemySpawner>(GetComponentsInChildren<EnemySpawner>());
    }

    public void spawn()
    {
        last_spawn_time = Time.time;
        int spawn_index = Random.Range(0, spawners.Count-1);
        House h = VM.getRandomHouse();
        if (h==null)
        { // no more houses, go for last villagers at evacpoint
            spawners[spawn_index].spawn( evacPoint );
            return;
        }
        spawners[spawn_index].spawn( h.transform );
    }

    public void reassign( Enemy e )
    {
        House h = VM.getRandomHouse();
        if (h==null)
        { // no more houses, go for last villagers at evacpoint
            e.setBaseTarget( evacPoint );
            return;
        }
        e.setBaseTarget( h.transform ); 
    }

    public virtual void notifed(GameObject iGO)
    {
        base.notified(iGO);
    }
}
 