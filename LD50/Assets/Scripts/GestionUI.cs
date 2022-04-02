using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionUI : MonoBehaviour
{
    public GameObject modules_group;
    private Module[] modules;

    // Start is called before the first frame update
    void Start()
    {
        modules = modules_group.GetComponentsInChildren<Module>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
