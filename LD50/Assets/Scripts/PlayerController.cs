using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("tweaks")]
    public float movespeed = 5.0f;
    public float rotspeed = 10.0f;
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
        float translate = Input.GetAxis("Vertical") * movespeed * (-1);
        float rot = Input.GetAxis("Horizontal") * rotspeed;

        translate *= Time.unscaledDeltaTime;
        rot *= Time.unscaledDeltaTime;

        transform.Translate( translate, 0, 0);
        transform.Rotate( 0, 0, rot);

    }
}
