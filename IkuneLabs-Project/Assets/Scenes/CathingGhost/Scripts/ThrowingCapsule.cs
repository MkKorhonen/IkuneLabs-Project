using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingCapsule : MonoBehaviour
{
    public GameObject capsule;
    public GameObject throwCapsule;

    public float capsuleSpeed;
    public float throwRate;
    private float lastThrowTime;

    private Camera cam;

    public static ThrowingCapsule instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //Check if the Capsule prefab is active, if it is it will call the ThrowCap function when you touch the screen and it has been more time from previous
        //throw than what is the current throwRate. 
        if (capsule.activeSelf == true)
        {
            if (Time.time - lastThrowTime >= throwRate)
                capsule.SetActive(true);
            if (Input.touchCount == 1)
            {
                if (Time.time - lastThrowTime >= throwRate)
                    ThrowCap();
            }
        }
        return;
    }

    void ThrowCap()
    {
        //Resets the lastThrowTime to current time and then spawns a capsule prefab that will move forward from camera.
        // After 3 seconds that capsule will get destroyd.
        lastThrowTime = Time.time;

        GameObject proj = Instantiate(throwCapsule, cam.transform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * capsuleSpeed;
        capsule.SetActive(false);
        Destroy(proj, 3.0f);
        capsule.SetActive(true);
    }
}
