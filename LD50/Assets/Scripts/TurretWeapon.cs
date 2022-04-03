using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretWeapon : MonoBehaviour
{
    public Image  cursor;
    public GameObject missileRef;

    [Header("tweaks")]
    public float lock_duration;
    public float shoot_time = 1f;
    public List<Enemy> locked_enemies;
    public List<Enemy> in_range_enemies;

    protected GunTip missile_spawn;
    protected Turret turret;
    private float last_shot_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        missile_spawn = GetComponentInChildren<GunTip>();
        turret = GetComponentInParent<Turret>();
        in_range_enemies = new List<Enemy>();
        locked_enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (turret.level > 0 && Time.time - last_shot_time > shoot_time)
        {
            Shoot();
            last_shot_time = Time.time;
        }

        List<Enemy> toclean = new List<Enemy>();
        foreach( Enemy e in in_range_enemies)
        {
            e.lock_on_elapsed_time = Time.time - e.lock_on_start_time;
            if ( e.lock_on_elapsed_time > lock_duration  )
            {
                locked_enemies.Add(e);
                toclean.Add(e);
            }
        }
        foreach(Enemy ec in toclean)
        {
            in_range_enemies.Remove(ec);
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
            new_missile.transform.rotation = turret.transform.rotation;
            // set target on missile
            TurretMissile turret_missile = new_missile.GetComponentInChildren<TurretMissile>();
            if (!!turret_missile)
            {
                turret_missile.target = e.transform;
                turret_missile.turret_weapon = this;
            }
            else
                DestroyImmediate(new_missile);
        }
    }

    public void notifyDeath(Enemy iE)
    {
        if (!locked_enemies.Remove(iE))
            in_range_enemies.Remove(iE);
    }

    
}
