using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : Subscriber
{
    public Image  cursor;
    public GameObject missileRef;


    [Header("tweaks")]
    public float lock_duration;
    public List<Enemy> locked_enemies;
    public List<Enemy> in_range_enemies;

    protected GunTip missile_spawn;
    protected PlayerController PC;

    public AudioClip sound_fire;
    public AudioClip sound_fizzle;

    private AudioSource sound_player;

    // Start is called before the first frame update
    void Start()
    {
        missile_spawn = GetComponentInChildren<GunTip>();
        PC = GetComponentInParent<PlayerController>();
        in_range_enemies = new List<Enemy>();
        locked_enemies = new List<Enemy>();

        sound_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0f)
        {
            Shoot();
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
        if (locked_enemies.Count > 0)
            sound_player.clip = sound_fire;
        else
            sound_player.clip = sound_fizzle;

        sound_player.Play();

        if (!missile_spawn)
        {
            Debug.LogError("Missing Missile Spawner reference.");
            return;
        }
        foreach(Enemy e in locked_enemies)
        {
            Transform target = null;
            if(e!=null)
                target = e.transform;
            else
                continue;

            GameObject new_missile = Instantiate(missileRef, missile_spawn.gameObject.transform.position, Quaternion.identity);
            new_missile.transform.rotation = PC.transform.rotation;
            // set target on missile
            PlayerMissile pm = new_missile.GetComponentInChildren<PlayerMissile>();
            if (!!pm)
            {
                pm.target = target;
                pm.PW = this;
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

    public override void notify(GameObject iGO)
    {
        Enemy e = iGO.GetComponent<Enemy>();
        if (e == null)
        {
            locked_enemies.RemoveAll( item => item == null);
            in_range_enemies.RemoveAll( item => item == null);
        }

        notifyDeath(e);
    }


}
