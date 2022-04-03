    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    private NavMeshAgent navmesh;
    private const string walk_anim_param = "walk";
    private Animator animator;

    public Transform target;
    private Rigidbody rb;

    private bool walk;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponentInChildren<Rigidbody>();
        walk = true;
    }

    // Update is called once per frame
    void Update()
    {
        walk = ( rb.velocity.magnitude > 0.0001f );
        animator.SetBool( walk_anim_param, walk);
        navmesh.destination = target.position;
    }
}
