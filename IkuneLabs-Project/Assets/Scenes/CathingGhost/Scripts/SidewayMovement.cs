using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewayMovement : MonoBehaviour
{
    private int directionValue;
    public float moveSpeed;
    public float travelTime;
    private Camera cam;
    private Vector3 player;
    private float lastTime;

    private void Start()
    {
        cam = Camera.main;
        lastTime = Time.time;
        directionValue = 1;
    }


    void Update()
    {
        player = cam.transform.position;
        player.y = -1;
        MoveGhost();
    }

    public void MoveGhost()
    {
        if (directionValue == 1)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue++;
            }
            else
            {
                transform.LookAt(player);
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }

        }


        if (directionValue == 2)
        {
            if (Time.time - lastTime > travelTime)
            {
                lastTime = Time.time;
                directionValue--;
            }
            else
            {
                transform.LookAt(player);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}
