using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Subscriber
{
    public int n_villagers = 5;
    public float time_between_spawns = 1f;
    public Transform spawn_location;
    public BoxCollider idle_area;
    public GameObject villagerRef;
    private List<GameObject> spawned_villagers;
    private float time_last_spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawned_villagers = new List<GameObject>();
        time_last_spawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ( ( Time.time - time_last_spawn) > time_between_spawns )
        {
            if ( spawned_villagers.Count < n_villagers )
            {
                spawn();
            }
        }
    }

    public void spawn()
    {
        time_last_spawn = Time.time;

        GameObject villager = Instantiate( villagerRef, spawn_location.position, Quaternion.identity);
        spawned_villagers.Add(villager);

        Villager v = villager.GetComponent<Villager>();
        v.obs = GetComponentInParent<VillageManager>();
        v.init();
        v.updateTarget( getNewIDLELocation(v) );
    }

    public Vector3 getNewIDLELocation( Villager v)
    {
        if (idle_area!=null)
        {
            Vector3 new_location = idle_area.center + 
            new Vector3
            (
                Random.Range( -idle_area.size.x/2, idle_area.size.x/2 ),
                Random.Range( -idle_area.size.y/2, idle_area.size.y/2 ),
                v.transform.position.z 
            );
            return new_location;
        } 
        return this.gameObject.transform.position; // default is house
    }

    public override void notify(GameObject iGO)
    {
        Villager v = iGO.GetComponent<Villager>();
        if (v==null)
            return;

        v.updateTarget( getNewIDLELocation(v) );
    }
}
