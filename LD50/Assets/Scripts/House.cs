using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Subscriber
{
    public enum HOUSE_ORDER
    {
        IDLE = 0,
        EVACUATE = 1
    };

    public int HP = 3;
    public int n_villagers = 5;
    public float time_between_spawns = 1f;
    public Transform spawn_location;
    public BoxCollider idle_area;
    public GameObject villagerRef;
    [HideInInspector]
    public GameObject evacuationPoint;

    public GameObject explosion;
    public GameObject warning;

    public HOUSE_ORDER order;

    public List<GameObject> spawned_villagers;
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

        // if (order == HOUSE_ORDER.EVACUATE)
        // {
        //     List<Villager> toclean = new List<Villager>();
        //     spawned_villagers.RemoveAll(item => item == null);
        //     foreach(GameObject go in spawned_villagers)
        //     {
        //         Villager v = go.GetComponent<Villager>();
        //         if ( !!v && v.evacuated)
        //         {
        //             toclean.Add(v);
        //         }
        //     }
        //     foreach(Villager v in toclean)
        //     {
        //         spawned_villagers.Remove(v.gameObject);
        //         Destroy(v.gameObject);
        //     }
        // }
    }

    public void spawn()
    {
        time_last_spawn = Time.time;

        GameObject villager = Instantiate( villagerRef, spawn_location.position, Quaternion.identity);
        spawned_villagers.Add(villager);

        Villager v = villager.GetComponent<Villager>();
        v.obs = GetComponentInParent<VillageManager>();
        v.home = this;
        v.init();
        applyOrder(v);
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

    public void applyOrder(Villager v)
    {
        if ( order == HOUSE_ORDER.EVACUATE)        
            v.updateTarget( evacuationPoint.transform.position );
        else
            v.updateTarget( getNewIDLELocation(v) );
    }

    public void applyOrderAll()
    {
        //
        foreach ( GameObject go in spawned_villagers)
        {
            Villager v = go.GetComponent<Villager>();
            if (v==null)
                continue;
                
            applyOrder(v);
        }

    }

    public void changeOrder( HOUSE_ORDER iNewOrder)
    {
        order = iNewOrder;
        spawned_villagers.RemoveAll(item => item == null);
        foreach( GameObject go in spawned_villagers )
        {
            Villager v = go.GetComponent<Villager>();
            if (!!v)
                applyOrder(v);
        }
    }

    public override void notify(GameObject iGO)
    {
        Villager v = iGO.GetComponent<Villager>();
        if (v==null)
            return;
        applyOrder(v);
    }

    public void damage()
    {
        HP--;
        if (HP<=0)
            kill();
    }

    public void kill()
    {
        spawned_villagers.RemoveAll(item => item == null);

        // send villagers to evacuation
        order = HOUSE_ORDER.EVACUATE;
        applyOrderAll();

        // unsubscribe, notify parents etc..
        // needed ?

        GameObject boom = Instantiate(explosion);
        boom.transform.position = transform.position;
        Instantiate(warning);

        // destroy itself
        Destroy(gameObject);
    }
}
