using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    private CameraBob cameraBob;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;

    //Variables used to Check if the Player is currently Grounded
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isGrounded;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cameraBob = transform.Find("Player Camera").GetComponent<CameraBob>();
    }

    void Update()
    {     
        //Checks to if Grounded, Then resets Velocity if True
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        //Applies Gravity to the Player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Movement Code
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.right * x + transform.forward * z;
        if (moveVector != Vector3.zero)
            cameraBob.isWalking = true;
        else
            cameraBob.isWalking = false;

        //Check if the Player is Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
            controller.Move(moveVector * (moveSpeed + 1.0f) * Time.deltaTime);
        else
            controller.Move(moveVector * moveSpeed * Time.deltaTime);
    }
}
