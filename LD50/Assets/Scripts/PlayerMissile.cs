using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public GameObject explosion;
    public float speed = 10f;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public PlayerWeapon PW;


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
            PW.notifyDeath(e);
            explode();
        }
    }

    private void explode()
    {
        Instantiate( explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
