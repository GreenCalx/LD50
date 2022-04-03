    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : Observed
{
    public float idle_duration = 2.0f;

    [HideInInspector]
    public bool evacuated = false;
    private NavMeshAgent navmesh;
    private const string walk_anim_param = "walk";
    private Animator animator;

    private NavMeshPath path;
    private Rigidbody rb;
    private float idle_elapsed_time;

    private bool walk;

    private bool inited = false;


    // Start is called before the first frame update
    void Start()
    {
        init();
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
}
