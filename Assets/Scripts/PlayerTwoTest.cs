using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoTest : MonoBehaviour
{

    public CharacterController controller;

    private Vector3 moveVector;
    public float gravity;
    public float verticalVelocity;
    public float horizontalVelocity;


    void Start()
    {
        controller = GetComponent<CharacterController>(); ;
    }

    void Update()
    {

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector.x = 0;
        moveVector.y = verticalVelocity;
        moveVector.z = 0f;

        controller.Move(moveVector * Time.deltaTime);

    }

    public void ReceiveDamage(string damagetype)
    {
        Debug.Log(damagetype);
        if (damagetype == "Normal")
        {
            //damage -= 10;
            //Agregar combo
        }
        else if (damagetype == "Up")
        {
            verticalVelocity = 20;
            moveVector.y = verticalVelocity;

            controller.Move(moveVector * Time.deltaTime);
        }
        else if (damagetype == "Down")
        {
            verticalVelocity = -20;
            moveVector.y = verticalVelocity;

            controller.Move(moveVector * Time.deltaTime);
        }
    }
}
