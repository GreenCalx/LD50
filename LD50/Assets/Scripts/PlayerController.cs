using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("tweaks")]
    public float movespeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        listenInputs();
    }

    public void listenInputs()
    {
        float vert = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        if ( vert > 0 )
        {
            
        } else if ( vert < 0)
        {

        }

        if ( hor > 0 )
        {

        } else if ( hor < 0 )
        {

        }
    }
}
