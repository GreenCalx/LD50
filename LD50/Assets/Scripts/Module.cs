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

    private AudioClip sound_start_build;
    private AudioClip sound_finish_build;
    protected AudioSource sound_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected void BaseStart()
    {
        help = GetComponentInParent<Modules>().help;
        GetComponent<MeshFilter>().mesh = models[level];

        sound_player = GetComponentInParent<AudioSource>();
        sound_start_build = GetComponentInParent<Modules>().sound_start_build;
        sound_finish_build = GetComponentInParent<Modules>().sound_finish_build;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void BaseUpdate()
    {
        if (is_building && (Time.time - build_start_time) >= building_time)
        {
            FinishBuild();
        }
    }

    public bool Start_build()
    {
        return BaseStart_build();
    }

    public bool BaseStart_build()
    {
        if (!is_building && (level < level_max))
        {
            build_start_time = Time.time;
            is_building = true;
            help.TargetModule(GetComponentInChildren<HelpAnchor>().transform);

            sound_player.clip = sound_start_build;
            sound_player.Play();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void Interrupt_build()
    {
        is_building = false;
    }

    public void FinishBuild()
    {
        is_building = false;
        level++;
        GetComponent<MeshFilter>().mesh = models[level];
        help.TargetPlayer();

        sound_player.clip = sound_finish_build;
        sound_player.Play();
    }
}
