using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public Transform target;
    public float distance = 0.2f;
    public GameObject player;
    public float speed = 1;

    public bool going_building = false;
    public bool is_building = false;
    public AudioClip sound_building;

    private AudioSource sound_player;

    // Start is called before the first frame update
    void Start()
    {
        sound_player = GetComponent<AudioSource>();
        sound_player.clip = sound_building;

        TargetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.1f * speed * Time.deltaTime * new Vector3(1 - 2 * (Random.value), 1 - 2 * (Random.value), 1 - 2 * (Random.value)));
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) > distance)
            transform.Translate(speed * Time.deltaTime * new Vector3(0, 0, 1));
        else if (going_building)
        {
            going_building = false;
            is_building = true;

            sound_player.Play();
        }

    }

    public void TargetPlayer()
    {
        target = player.transform;
        is_building = false;
        going_building = false;

        sound_player.Stop();
    }

    public void TargetModule(Transform target_transform)
    {
        target = target_transform;
        going_building = true;
        is_building = false;
    }
}
