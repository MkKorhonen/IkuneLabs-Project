using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    private int directionValue;
    public float moveSpeed;
    private Camera cam;
    private Vector3 player;
    public float width;
    public float height;
    private float timeC;
    

    private void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        player = cam.transform.position;
        player.y = -1;
        MoveGhost();
    }

    public void MoveGhost()
    {
        timeC += Time.deltaTime * moveSpeed;
        float x = Mathf.Cos(timeC) * width;
        float y = Mathf.Sin(timeC) * height;

        //transform.LookAt(player);
        transform.position = new Vector3(x, y, 5);
    }
}
