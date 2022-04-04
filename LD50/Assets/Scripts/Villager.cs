using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : Observed
{
    public float idle_duration = 2.0f;

    [HideInInspector]
    public bool evacuated = false;
    [HideInInspector]
    public bool is_dead = false;
    private NavMeshAgent navmesh;
    private const string walk_anim_param = "walk";
    private Animator animator;

    private NavMeshPath path;
    private Rigidbody rb;
    private float idle_elapsed_time;

    private bool walk;

    private bool inited = false;

    public AudioClip[] sounds;
    private float last_sound_time = 0;
    public float time_between_sounds = 60;
    private AudioSource sound_player;


    // Start is called before the first frame update
    void Start()
    {
        init();

        sound_player = GetComponent<AudioSource>();
        makeSound();
    }

    public void init()
    {
        if (inited)
            return;

        animator = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponentInChildren<Rigidbody>();
        walk = false;
        path = new NavMeshPath();
        idle_elapsed_time = 0f;
        evacuated = false;
        is_dead = false;
        inited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ( obs == null )
            return;
        
        if (navmesh.remainingDistance == 0f)
        {
            if (walk)
                idle_elapsed_time = 0f;
            walk = false;

            if ( idle_elapsed_time > idle_duration )
            {
                notify(); // request new target position
            } else {
                idle_elapsed_time += Time.deltaTime;
            }
            
        } else {
            walk = true;
        }
        
        animator.SetBool( walk_anim_param, walk);
        //rb.gameObject.transform.LookAt(target);

        if ((sound_player != null) && (sounds.Length > 0) && (Time.time - last_sound_time > time_between_sounds))
        {
            makeSound();
        }
    }

    protected void makeSound()
    {
        sound_player.clip = sounds[Random.Range(0, sounds.Length)];
        sound_player.Play();

        last_sound_time = Time.time;
    }

    public void updateTarget( Vector3 iTarget)
    {
        NavMesh.CalculatePath(transform.position, iTarget, NavMesh.AllAreas, path);
        navmesh.path = path;
    }

    public void quitVillage()
    {
        evacuated = true;
    }

    public void kill()
    {
        Debug.Log("villager KILLED");
        Destroy(gameObject);
    }
}
