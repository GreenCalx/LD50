using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public List<Subscriber> subs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void notified(GameObject iGO)
    {
        foreach(Subscriber s in subs)
        {
            s.notify( iGO );
        }
    }
}
