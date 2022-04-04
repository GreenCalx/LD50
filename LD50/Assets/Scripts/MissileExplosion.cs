using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{

    public float explosion_duration = 2f;
    private double start_time;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource sound_player = GetComponent<AudioSource>();
        sound_player.clip = sounds[Random.Range(0, sounds.Length)];
        sound_player.Play();

        start_time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Time.time - start_time) > explosion_duration )
        {
            Destroy(this.gameObject);
        }
    }
}
