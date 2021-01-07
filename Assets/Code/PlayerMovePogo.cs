using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovePogo : MonoBehaviour
{
    //player controller
    public CharacterController controller;

    //grounded stuff
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;//BE SURE TO USE THIS LAYER ON ANYTHING YOU'RE STEPPING ON.
    bool isGrounded;

    //movement variables
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        //grounded stuff.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //inputs
        float x = Input.GetAxis("Horizontal");
        float z = 1;

        //move based on camera look
        Vector3 move = transform.right * x + transform.forward * z;
        //move the player horizontally.
        controller.Move(move * speed * Time.deltaTime);

        //jump when you hit the ground
        if (isGrounded)
        {
            //jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //change direction of forward movement to wherever the cursor is pointing at
        }

        //apply gravity.
        velocity.y += gravity * Time.deltaTime;

        //make him fall every frame at this velocity.
        controller.Move(velocity * Time.deltaTime);
    }
}