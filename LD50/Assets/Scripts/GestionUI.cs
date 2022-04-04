using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUI : MonoBehaviour
{
    public GameObject gestionCursor;
    public Color basic;
    public Color selected;
    public Color building;
    private List<KeyValuePair<GameObject, Module>> module_cursors;
    private int selection_id;
    public int latch;
    private int frame_count = 0;
    public AudioClip sound_select;
    public AudioClip sound_nope;
    public AudioClip sound_build_instruction;
    private AudioSource sound_player;

    // Start is called before the first frame update
    void Start()
    {
        module_cursors = new List<KeyValuePair<GameObject, Module>>();
        Module[] modules = GetComponentInParent<ModeManager>().modules_group.GetComponentsInChildren<Module>();
        Camera cam = GetComponentInParent<ModeManager>().gestion_cam;
        //Vector2 UIsizes = GetComponent<CanvasScaler>().referenceResolution;
        Vector2 UIsizes = new Vector2(Screen.width, Screen.height);
        foreach (Module module in modules)
        {
            Vector3 position = cam.WorldToScreenPoint(module.transform.position);
            GameObject cursor = Instantiate(gestionCursor, transform);
            cursor.GetComponent<RectTransform>().anchoredPosition = position - new Vector3(UIsizes.x / 2, UIsizes.y / 2, 0);
            module_cursors.Add(new KeyValuePair<GameObject, Module>(cursor, module));
        }

        sound_player = GetComponent<AudioSource>();

        selection_id = 0;
        Select(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (frame_count > latch)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
                Select(1);
            else if (Input.GetAxisRaw("Horizontal") < 0)
                Select(-1);
            else if (Input.GetAxisRaw("Fire1") > 0)
                Build();
            frame_count = 0;
        }
        frame_count++;

        for (int id = 0; id < module_cursors.Count; id++)
        {
            if (id == selection_id)
                module_cursors[id].Key.GetComponent<RawImage>().color = selected;
            else if (module_cursors[id].Value.is_building)
                module_cursors[id].Key.GetComponent<RawImage>().color = building;
            else
                module_cursors[id].Key.GetComponent<RawImage>().color = basic;
        }

    }

    public void Select(int direction)
    {
        if (module_cursors.Count > 0)
        {
            selection_id = Math.Modulo((selection_id + direction), module_cursors.Count);

            if (direction != 0)
            {
                sound_player.clip = sound_select;
                sound_player.Play();
            }
        }
    }

    private void Build()
    {
        if (module_cursors[selection_id].Value.Start_build())
        {
            for (int id = 0; id < module_cursors.Count; id++)
            {
                if (id != selection_id)
                {
                    module_cursors[id].Value.Interrupt_build();
                }
            }

            sound_player.clip = sound_build_instruction;
            sound_player.Play();
        }
        else
        {
            sound_player.clip = sound_nope;
            sound_player.Play();
        }
    }
}
