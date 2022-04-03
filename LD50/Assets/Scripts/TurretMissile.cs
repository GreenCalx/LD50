using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 10f;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public TurretWeapon turret_weapon;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            chase();
        } else {
            explode();
        }
    }

    public void chase()
    {
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards( transform.position, target.position, step);
    }

    void OnTriggerEnter(Collider iCollider)
    {
        Enemy e = iCollider.GetComponent<Enemy>();
        if (e!=null)
        {
            e.Die();
            turret_weapon.notifyDeath(e);
            explode();
        }
    }

    private void explode()
    {
        Instantiate( explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        Destroy(transform.parent.gameObject);
    }
}
