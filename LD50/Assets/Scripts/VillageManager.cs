using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : Observer
{
    public GameObject rocketRef;
    public GameObject evacPoint;
    public GameOver gameOver;
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
            if (!evac.evacuate)
                evacuate(true);
            evac.evacuate = true;
            
        } else {
            if (evac.evacuate)
                evacuate(false);
            evac.evacuate = false;
        }

        if (houses.Count == 0)
            gameOver.Trigger();
    }

    public House getRandomHouse()
    {
        refreshHouses();
        if (houses.Count==0)
            return null;
        int i = Random.Range( 0, houses.Count);
        return houses[i];
    }

    public void evacuate(bool iState)
    {
        foreach( House h in houses)
        {
            h.changeOrder( (iState) ? House.HOUSE_ORDER.EVACUATE : House.HOUSE_ORDER.IDLE );
        }
    }

    public virtual void notifed(GameObject iGO)
    {
        base.notified(iGO);
    }
}
