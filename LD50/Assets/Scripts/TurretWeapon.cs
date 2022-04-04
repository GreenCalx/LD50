using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretWeapon : Subscriber
{
    public Image  cursor;
    public GameObject missileRef;

    [Header("tweaks")]
    public float lock_duration;
    public float shoot_time = 1f;
    public List<Enemy> in_range_enemies;

    protected GunTip missile_spawn;
    protected Turret turret;
    private float last_shot_time = 0f;
    
    public AudioClip sound_fire;

    private AudioSource sound_player;

    // Start is called before the first frame update
    void Start()
    {
        missile_spawn = GetComponentInChildren<GunTip>();
        turret = GetComponentInParent<Turret>();
        in_range_enemies = new List<Enemy>();

        sound_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (turret.level > 0 && Time.time - last_shot_time > shoot_time)
        {
            Shoot();
            last_shot_time = Time.time;
        }
    }

    public void Shoot()
    {
        if (!missile_spawn)
        {
            Debug.LogError("Missing Missile Spawner reference.");
            return;
        }

        if (in_range_enemies.Count<=0)
            return;

        int target_id = Random.Range( 0, in_range_enemies.Count-1);
        Enemy e = in_range_enemies[target_id];
        if (e!=null)
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
        in_range_enemies.Remove(iE);
    }

    public override void notify(GameObject iGO)
    {
        Enemy e = iGO.GetComponent<Enemy>();
        if (e == null)
            return;

        notifyDeath(e);
    }
}
