using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUI : MonoBehaviour
{
    public GameObject modules_group;
    public GameObject gestionCursor;
    private Module[] modules;
    private List<GameObject> cursors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        modules = modules_group.GetComponentsInChildren<Module>();
        Camera cam = GetComponentInParent<ModeManager>().gestion_cam;
        foreach (Module module in modules)
        {
            Vector3 position = cam.WorldToScreenPoint(module.transform.position);
            GameObject cursor = Instantiate(gestionCursor, transform);
            Vector2 UIsizes = GetComponent<CanvasScaler>().referenceResolution;
            cursor.GetComponent<RectTransform>().anchoredPosition = position - new Vector3(UIsizes.x / 2, UIsizes.y / 2, 0);
            cursors.Add(cursor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
