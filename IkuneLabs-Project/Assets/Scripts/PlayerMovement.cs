using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    public float speed = 50f;
    public float mobileSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        characterController.Move(moveDirection * Time.deltaTime);

        //Touch movement
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                moveDirection = new Vector3(touch.deltaPosition.x, 0.0f, touch.deltaPosition.y);
                moveDirection *= mobileSpeed;

                characterController.Move(moveDirection * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ghost")
        {
            //Add transition to ghost catching scene
            Debug.Log("Touching ghost");
        }
    }
}
