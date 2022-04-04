using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spider : Enemy
{
    private Animator animator;
    private const string walk_anim_param = "walk";
    private const string atk_anim_param = "atk";

    private bool is_walking = false;
    private bool is_atking = false;

    // Start is called before the first frame update
    void Start()
    {
        //inited = false;
        //init();
        BaseStart();

        animator = GetComponent<Animator>();
        is_walking = false;
        is_atking = false;
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        is_walking = (path.status == NavMeshPathStatus.PathComplete );
    }

    void FixedUpdate()
    {
        animator.SetBool( walk_anim_param, is_walking);
        animator.SetBool( atk_anim_param, is_atking);
    }

    public override void onDamageDealt()
    {
        base.onDamageDealt();
        is_atking = true;
    }

    public override void outOfDamageRange()
    {
        base.outOfDamageRange();
        is_atking = false;
    }
}
