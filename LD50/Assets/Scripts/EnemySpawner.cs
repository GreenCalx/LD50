using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public int n_spawns = 10;
    public List<Enemy> spawnables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn( Transform target)
    {
        for (int i=0; i < n_spawns; i++)
        {
            int enemy_type = Random.Range(0, spawnables.Count-1);
            Enemy e = spawnables[enemy_type];
            //GameObject new_e_go = Instantiate( e.gameObject, transform.position, Quaternion.identity);
            NavMeshHit closestHit;
            if( NavMesh.SamplePosition( transform.position, out closestHit, 50, NavMesh.AllAreas) )
            {
                GameObject new_e_go = Instantiate( e.gameObject, closestHit.position, Quaternion.identity);
                new_e_go.transform.SetParent(transform.parent);
                Enemy new_e = new_e_go.GetComponent<Enemy>();
                if (!!new_e)
                {
                    new_e.init();
                    new_e.setBaseTarget(target);
                    new_e.setTarget( target );
                }
            }
            

        }
    }

}
