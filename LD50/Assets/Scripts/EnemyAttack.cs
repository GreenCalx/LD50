using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Enemy parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateTarget( Collider iCollider)
    {
        // PRIO 1 : ATK VILLAGER
        Villager v = iCollider.GetComponent<Villager>();
        if (!!v)
        {
            parent.setTarget(v.transform);
            return;
        }

        // PRIO 2 : ATK HOUSE
        House h = iCollider.GetComponent<House>();
        if (!!h)
        {
            parent.setTarget(h.transform);
            return;
        }

        // PRIO 3 : ATK PLAYER
        PlayerController pc = iCollider.GetComponent<PlayerController>();
        if (!!pc)
        {
            parent.setTarget(pc.transform);
            return;
        }

        // PRIO 4 : ATK MODULE
        // TODO
    }

    void OnTriggerEnter( Collider iCollider)
    {
        updateTarget(iCollider);
    }

    void OnTriggerStay( Collider iCollider)
    {
        if (iCollider.GetComponent<WeaponRange>())
            return;

        updateTarget(iCollider);
    }
    void OnTriggerExit( Collider iCollider)
    {
        if (iCollider.GetComponent<WeaponRange>())
            return;
        parent.resetTarget();
    } 
}
