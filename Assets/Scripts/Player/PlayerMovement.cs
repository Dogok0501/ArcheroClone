using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] JoystickController joystick;

    new Rigidbody rigidbody;

    float horizontalMovement;
    float verticalMovement;

    float angle;   

    public bool isMoving;

    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementInput();
        PlayerHeading();
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(horizontalMovement * movementSpeed, rigidbody.velocity.y, verticalMovement * movementSpeed);
    }

    void MovementInput()
    {
        horizontalMovement = joystick.inputVector.x;
        verticalMovement = joystick.inputVector.y;

        if (horizontalMovement != 0 || verticalMovement != 0)
            isMoving = true;
        else
            isMoving = false;
    }

    void PlayerHeading()
    {
        if(isMoving)
        {
            angle = Mathf.Atan2(joystick.inputVector.x, joystick.inputVector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, angle, 0)), Time.deltaTime * rotationSpeed);
        }        
    }
}
