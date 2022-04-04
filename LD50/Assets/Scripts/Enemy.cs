using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy :  Observed
{
    public Vector3 target;
    public bool is_locked_by_player;
    public double lock_on_start_time;
    public double lock_on_elapsed_time;
    public AudioClip[] sounds;
    public float time_between_sounds = 10;

    protected NavMeshAgent navmesh;
    protected NavMeshPath path;
    protected bool inited = false;
    protected Transform baseTarget;
    private bool is_dead = false;
    private float last_sound_time = 0;
    private AudioSource sound_player;


    // Start is called before the first frame update
    protected void BaseStart()
    {
        inited = false;
        init();

        sound_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void BaseUpdate()
    {
        if ((sound_player != null) && (sounds.Length > 0) && (Time.time - last_sound_time > time_between_sounds))
        {
            sound_player.clip = sounds[Random.Range(0, sounds.Length)];
            sound_player.Play();

            last_sound_time = Time.time;
        }
    }

    public virtual void onDamageDealt()
    {

    }

    public virtual void outOfDamageRange()
    {
        
    }

    public void init()
    {
        is_dead = false;
        obs = GetComponentInParent<EnemyManager>();
        is_locked_by_player = false;
        navmesh = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        inited = true;
    }

    public void setBaseTarget( Transform iBaseTarget)
    {
        baseTarget = iBaseTarget;
    }

    public void setTarget( Transform iTarget)
    {
        target = iTarget.position;
        updateTarget( target );
    }

    public void updateTarget( Vector3 iTarget)
    {
        if (!inited)
            init();
        if (is_dead)
            return;

        NavMesh.CalculatePath(transform.position, iTarget, NavMesh.AllAreas, path);
        navmesh.path = path;
    }

    public void resetTarget()
    {
        if (!!baseTarget)
            updateTarget(baseTarget.position);
        else
        { 
            GetComponentInParent<EnemyManager>().reassign(this);
        }
    }

    public void Die()
    {
        notify();
        is_dead = true;
        Destroy(this.gameObject);
    }
}
