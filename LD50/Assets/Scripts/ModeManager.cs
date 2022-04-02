using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum Mode
    {
        real_time,
        gestion
    }
    public Mode mode;
    public Camera main_cam;
    public Camera gestion_cam;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        main_cam.enabled = true;
        gestion_cam.enabled = false;
        mode = Mode.real_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switch (mode)
            {
                case Mode.real_time:
                    mode = Mode.gestion;
                    main_cam.enabled = false;
                    gestion_cam.enabled = true;
                    Time.timeScale = 0;
                    break;

                case Mode.gestion:
                    mode = Mode.real_time;
                    main_cam.enabled = true;
                    gestion_cam.enabled = false;
                    Time.timeScale = 1;
                    break;
            }
        }
    }
}
