using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evacuation : MonoBehaviour
{
    public bool evacuate = false;
    [HideInInspector]
    public Rocket rocket;
    // Start is called before the first frame update
    void Start()
    {
        evacuate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider iCollider)
    {
        if (!evacuate)
            return;

        Villager v = iCollider.GetComponentInParent<Villager>();
        if (!!v)
        {
            if (rocket.Embark())
            {
                v.quitVillage();
            }
        }
    }
}
