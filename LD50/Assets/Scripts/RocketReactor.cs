using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketReactor : MonoBehaviour
{
    public ParticleSystem PE;

    // Start is called before the first frame update
    void Start()
    {
        PE = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stop()
    {
        PE.Stop();
    }

    public void play()
    {
        PE.Play();
    }
}
