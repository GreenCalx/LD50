using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public int level = 0;
    public int level_max = 1;
    public int building_time = 10;
    public bool is_building = false;
    public float build_start_time;
    public Mesh[] models;
    private Help help;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void BaseStart()
    {
        help = GetComponentInParent<Modules>().help;
        FinishBuild();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void BaseUpdate()
    {
        if (is_building && (Time.time - build_start_time) >= building_time)
        {
            LevelUp();
        }
    }

    public void Start_build()
    {
        if (!is_building && (level < level_max))
        {
            build_start_time = Time.time;
            is_building = true;
            help.target = transform;
        }
    }

    public void Interrupt_build()
    {
        is_building = false;
    }

    public void LevelUp()
    {
        is_building = false;
        level++;
        FinishBuild();
    }

    public void FinishBuild()
    {
        GetComponent<MeshFilter>().mesh = models[level];
        help.TargetPlayer();
    }
}
