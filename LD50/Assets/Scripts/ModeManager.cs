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
    private GestionUI gestionUI;

    public GameObject player;
    public GameObject modules_group;

    public float latch = 0.1f;
    private float elapsed_time = 0;
    private bool has_changed_mode = false;

    // Start is called before the first frame update
    void Start()
    {
        main_cam.enabled = true;
        gestion_cam.enabled = false;
        gestionUI = GetComponentInChildren<GestionUI>();
        gestionUI.enabled = false;
        mode = Mode.real_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!has_changed_mode)
        {
            if (Input.GetAxisRaw("Fire2") > 0)
            {
                switch (mode)
                {
                    case Mode.real_time:
                        mode = Mode.gestion;
                        main_cam.enabled = false;
                        gestion_cam.enabled = true;
                        gestionUI.enabled = true;
                        //gestionUI.Select(0);
                        Time.timeScale = 0;
                        break;

                    case Mode.gestion:
                        mode = Mode.real_time;
                        main_cam.enabled = true;
                        gestion_cam.enabled = false;
                        gestionUI.enabled = false;
                        Time.timeScale = 1;
                        break;
                }
                has_changed_mode = true;
            }
        }
        else
        {
            elapsed_time += Time.unscaledDeltaTime;
            if (elapsed_time > latch)
            {
                has_changed_mode = false;
                elapsed_time = 0;
            }
        }
    }
}
