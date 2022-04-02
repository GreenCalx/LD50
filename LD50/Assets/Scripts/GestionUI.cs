using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUI : MonoBehaviour
{
    public GameObject modules_group;
    public GameObject gestionCursor;
    public Color basic;
    public Color selected;
    private List<KeyValuePair<GameObject, Module>> module_cursors;
    private int selection_id;

    // Start is called before the first frame update
    void Start()
    {
        module_cursors = new List<KeyValuePair<GameObject, Module>>();
        Module[] modules = modules_group.GetComponentsInChildren<Module>();
        Camera cam = GetComponentInParent<ModeManager>().gestion_cam;
        Vector2 UIsizes = GetComponent<CanvasScaler>().referenceResolution;
        foreach (Module module in modules)
        {
            Vector3 position = cam.WorldToScreenPoint(module.transform.position);
            GameObject cursor = Instantiate(gestionCursor, transform);
            cursor.GetComponent<RectTransform>().anchoredPosition = position - new Vector3(UIsizes.x / 2, UIsizes.y / 2, 0);
            cursor.GetComponent<RawImage>().color = basic;
            module_cursors.Add(new KeyValuePair<GameObject, Module>(cursor, module));
        }
        selection_id = 0;
        Select(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Select(1);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Select(-1);
    }

    private void Select(int direction)
    {
        module_cursors[selection_id].Key.GetComponent<RawImage>().color = basic;
        selection_id = Math.Modulo((selection_id + direction), module_cursors.Count);
        module_cursors[selection_id].Key.GetComponent<RawImage>().color = selected;
        
    }
}
