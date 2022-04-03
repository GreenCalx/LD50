using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : Observer
{
    public GameObject rocketRef;
    public GameObject evacPoint;
    private Rocket rocket;
    private Evacuation evac;

    private List<House> houses;


    // Start is called before the first frame update
    void Start()
    {
        rocket = rocketRef.GetComponent<Rocket>();
        evac = evacPoint.GetComponent<Evacuation>();
        evac.rocket = rocket;
        refreshHouses();
    }


    public void refreshHouses()
    {
        houses = new List<House>(GetComponentsInChildren<House>());
        foreach( House h in houses)
        {
            h.evacuationPoint = evacPoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( rocket.isReady() )
        {
            evacuate(true);
        } else {
            evacuate(false);
            evac.evacuate = false;
        }
    }

    public void evacuate(bool iState)
    {
        evac.evacuate = true;
        foreach( House h in houses)
        {
            h.order = (iState) ? House.HOUSE_ORDER.EVACUATE : House.HOUSE_ORDER.IDLE;
        }
    }

    public virtual void notifed(GameObject iGO)
    {
        base.notified(iGO);
    }
}
