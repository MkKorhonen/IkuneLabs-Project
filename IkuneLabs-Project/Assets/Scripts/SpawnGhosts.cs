using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhosts : MonoBehaviour
{
    public GameObject target;
    public GameObject ghost;
    public int timer = 3;
    public int maxSpawns = 3;
    public bool canSpawn = false;
    private Bounds bounds;
    private float x, y, z;
    

    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<Collider>().bounds;
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnTimer()
    {
        while(maxSpawns > 0)
        {
            yield return new WaitForSeconds(timer);

            if (canSpawn)
            {
                maxSpawns--;
                SpawnGhost();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canSpawn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canSpawn = false;
        }
    }

    void SpawnGhost()
    {
        x = Random.Range(-bounds.extents.x, bounds.extents.x);
        y = Random.Range(-bounds.extents.y, bounds.extents.y);
        z = Random.Range(-bounds.extents.z, bounds.extents.z);
        Instantiate(ghost, bounds.center + new Vector3(x, y, z), Quaternion.identity);
    }
}
