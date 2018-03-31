using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce = 20;
    public CharacterController controller;
    public Vector3 moveVector;
    public float gravity;
    public float verticalVelocity;

    public float dashDistance;
    public float dashSpeed;
    public float dashTimer;
    public float dashingTime;
    public bool isDashing;

    private IMove _iMove;
    private IAttack _iAttack;

    public Collider[] attackColliders;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dashSpeed = 50;
        dashDistance = 7;
        dashTimer = dashDistance / dashSpeed;
    }

    void Update()
    {
        Move();
        Dash();
        Attack();

    }

    void Move()
    {
        if (controller.isGrounded)
            verticalVelocity = -gravity * Time.deltaTime;
        else
            verticalVelocity -= gravity * Time.deltaTime;

        if (InputManager.AButton() && controller.isGrounded)
        {
            _iMove = new Jump();
            _iMove.Move(this);
        }

        else if (dashingTime == 0)
        {
            _iMove = new HorizontalMovement();
            _iMove.Move(this);
        }

        if (InputManager.LBButton() && !isDashing)
        {
            _iMove = new Dash();
            _iMove.Move(this);
        }
        controller.Move(moveVector * Time.deltaTime);
    }

    void Dash()
    {
        if (isDashing && dashingTime <= dashTimer)
            dashingTime += Time.deltaTime;
        else
            dashingTime = 0;

        if (controller.isGrounded)
            isDashing = false;
    }

    void Attack()
    {
        if (InputManager.XButton())
        {
            Debug.Log("Normal_J");
            _iAttack = new NormalAttack();
            _iAttack.Attack(attackColliders[0], transform);
        }

        if (InputManager.YButton())
        {
            Debug.Log("Up_J");
            _iAttack = new UpAttack();
            _iAttack.Attack(attackColliders[0], transform);
        }

        if (InputManager.BButton())
        {
            Debug.Log("Down_J");
            _iAttack = new DownAttack();
            _iAttack.Attack(attackColliders[0], transform);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && Vector3.Angle(transform.up, hit.normal) == 180)
        {
            if (verticalVelocity != 0)
                verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }
}
