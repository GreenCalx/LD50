using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    public Image  cursor;
    public GameObject missileRef;


    [Header("tweaks")]
    public float lock_duration;
    protected List<Enemy> locked_enemies;
    protected List<Enemy> in_range_enemies;

    protected GunTip missile_spawn;

    // Start is called before the first frame update
    void Start()
    {
        missile_spawn = GetComponentInChildren<GunTip>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("Fire1"))
        {
            Shoot();
        }

        foreach( Enemy e in in_range_enemies)
        {
            e.lock_on_elapsed_time = e.lock_on_start_time - Time.time;
            if ( e.lock_on_elapsed_time > lock_duration  )
            {
                locked_enemies.Add(e);
                in_range_enemies.Remove(e);
            }
        }
    }

    public void Shoot()
    {
        if (!missile_spawn)
        {
            Debug.LogError("Missing Missile Spawner reference.");
            return;
        }
        foreach(Enemy e in locked_enemies)
        {
            GameObject new_missile = Instantiate(missileRef, missile_spawn.gameObject.transform.position, Quaternion.identity);
            // set target on missile
        }
    }

    void OnTriggerEnter(Collider iCollider)
    {
        Enemy e = iCollider.GetComponent<Enemy>();
        if ( e != null)
        {
            if (!in_range_enemies.Contains(e))
            { 
                in_range_enemies.Add(e); 
                e.is_locked_by_player  = true;
                e.lock_on_start_time = Time.time;
            }
        }
    }

    void OnTriggerStay(Collider iCollider)
    {
        Enemy e = iCollider.GetComponent<Enemy>();
        if ( (e!=null) && !e.is_locked_by_player)
        {
            if (in_range_enemies.Contains(e))
            { 
                e.is_locked_by_player  = true; 
                e.lock_on_start_time = Time.time; 
            } 
            else
            {  
                in_range_enemies.Add(e); 
                e.is_locked_by_player  = true;
                e.lock_on_start_time = Time.time; 
            }        
        }
    }

    void OnTriggerExit(Collider iCollider)
    {
        Enemy e = iCollider.GetComponent<Enemy>();
        if ( e != null)
        {
            in_range_enemies.Remove(e); // if it was just in lock range
            locked_enemies.Remove(e); // if it was locked
            e.is_locked_by_player = false;
            e.lock_on_start_time = 0;
        }
    }
}
