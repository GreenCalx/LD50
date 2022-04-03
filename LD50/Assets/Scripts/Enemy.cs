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

    protected NavMeshAgent navmesh;
    protected NavMeshPath path;
    protected bool inited = false;
    protected Transform baseTarget;
    private bool is_dead = false;


    // Start is called before the first frame update
    void Start()
    {
        inited = false;
        init();
    }

    // Update is called once per frame
    void Update()
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
        updateTarget(baseTarget.position);
    }

    public void Die()
    {
        notify();
        is_dead = true;
        Destroy(this.gameObject);
    }
}
