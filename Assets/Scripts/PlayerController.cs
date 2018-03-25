using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveVector;
    public float gravity;
    public float verticalVelocity;

    public float dashDistance;
    public float dashSpeed;
    public float dashTimer;
    public float dashingTime;
    public bool isDashing;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        dashSpeed = 50;
        dashDistance = 7;
        dashTimer = dashDistance / dashSpeed;
    }

    void Update()
    { 

        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            moveVector.x = Mathf.Sign(moveVector.x) * dashSpeed;
            verticalVelocity = 0;
            moveVector.y = verticalVelocity;
            moveVector.z = 0f;
        }

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if(isDashing && dashingTime <= dashTimer)
        {
            dashingTime += Time.deltaTime;
        }

        else
        {
            if (controller.isGrounded)
                isDashing = false;
            dashingTime = 0;
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
            moveVector.y = verticalVelocity;
            moveVector.z = 0f;
        }
        
        controller.Move(moveVector * Time.deltaTime);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && Vector3.Angle(transform.up, hit.normal) == 180)
        {
            //Opcion I
            //verticalVelocity = -Mathf.Abs(verticalVelocity / 2);

            //Opcion II
            if (verticalVelocity != 0)
                verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }
}
