using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeaponRange : MonoBehaviour
{
    private TurretWeapon turret_weapon;

    // Start is called before the first frame update
    void Start()
    {
        turret_weapon = GetComponentInParent<TurretWeapon>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider iCollider)
    {
        Enemy e = iCollider.GetComponent<Enemy>();
        if ( e != null)
        {
            if (!turret_weapon.in_range_enemies.Contains(e))
            { 
                turret_weapon.in_range_enemies.Add(e); 
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
            if (turret_weapon.in_range_enemies.Contains(e))
            { 
                e.is_locked_by_player  = true; 
                e.lock_on_start_time = Time.time; 
            } 
            else
            {  
                turret_weapon.in_range_enemies.Add(e); 
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
            turret_weapon.in_range_enemies.Remove(e); // if it was just in lock range
            turret_weapon.locked_enemies.Remove(e); // if it was locked
            e.is_locked_by_player = false;
            e.lock_on_start_time = 0;
        }
    }
}
