using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerToggle : MonoBehaviour
{
    public LineRenderer laser;
    public GameObject gun;

    void Start()
    {
        laser.enabled = false;
    }

   
    void Update()
    {
        if (Input.touchCount > 0 && gun.activeSelf == true)
        {
            laser.enabled = true;
        }
        else
            laser.enabled = false;
    }
}
