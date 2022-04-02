using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observed : MonoBehaviour
{
    public Observer obs;

    void Start()
    {
        obs = GetComponentInParent<Observer>();
    }
    void Update()
    {
    }

    public void notify()
    {
        obs.notified(this.gameObject);
    }


}
