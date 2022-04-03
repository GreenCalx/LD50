using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float gcd = 2f;
    private float last_damage_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        last_damage_time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void doDamage(GameObject iGO)
    {
        if ((Time.time - last_damage_time) < gcd)
            return;

        Villager v = iGO.GetComponent<Villager>();
        PlayerController pc = iGO.GetComponent<PlayerController>();
        House h = iGO.GetComponent<House>();
        if (!!v)
        {
            v.kill();
        }
        if (!!pc)
        {
            pc.kill();
        }
        if (!!h)
        {
            h.damage();
        }
        last_damage_time = Time.time;
    }

    void OnTriggerEnter( Collider iCollider)
    {
        doDamage(iCollider.gameObject);
    }

    void OnTriggerStay( Collider iCollider)
    {
        doDamage(iCollider.gameObject);
    }
}
