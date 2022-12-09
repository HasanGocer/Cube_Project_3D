using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;

    [SerializeField] private float movementSpeed;

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        rigidbody.velocity = new Vector3(joystick.Horizontal * movementSpeed, rigidbody.velocity.y, joystick.Vertical * movementSpeed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }
    }
}
