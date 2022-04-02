using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public Camera main_cam;
    public Camera gestion_cam;

    // Start is called before the first frame update
    void Start()
    {
        main_cam.enabled = true;
        gestion_cam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            main_cam.enabled = !main_cam.enabled;
            gestion_cam.enabled = !gestion_cam.enabled;
        }
    }
}
