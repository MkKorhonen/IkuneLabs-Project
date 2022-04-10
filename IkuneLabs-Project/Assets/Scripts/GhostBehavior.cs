using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public Transform player;
    public int moveSpeed = 5;
    public int MinDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if (Vector3.Distance(transform.position, player.position) >= MinDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
