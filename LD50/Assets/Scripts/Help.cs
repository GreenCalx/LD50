using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public Transform target;
    public float distance = 0.2f;
    public GameObject player;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) < distance)
        {
            transform.Translate(speed * Time.deltaTime * new Vector3(1 - 2 * (Random.value), 1 - 2 * (Random.value), 1 - 2 * (Random.value)));
        }
        else
        {
            transform.Translate(speed * Time.deltaTime * new Vector3(0, 0, 1));
        }
    }

    public void TargetPlayer()
    {
        target = player.transform;
    }
}
