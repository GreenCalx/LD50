using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : Observer
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void notifed(GameObject iGO)
    {
        base.notified(iGO);
    }
}
