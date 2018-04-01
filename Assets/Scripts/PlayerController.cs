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

    public float dashCoolDown;
    public bool isDashing;
    public bool canDash = true;

    private IMove _iMove;
    private IAttack _iAttack;

    public Collider[] attackColliders;
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dashCoolDown = 3f;

        hability.Add(typeof(Dash).ToString(), new Dash(this, dashCoolDown));
    }

    void Update()
    {
        Move();
        Habilities();
        Attack();
        controller.Move(moveVector * Time.deltaTime);
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

        else if (!isDashing)
        {
            _iMove = new HorizontalMovement();
            _iMove.Move(this);
        }
    }

    void Habilities()
    {
        foreach (var h in hability.Values)
            h.Update();

        if (InputManager.LBButton() && canDash)
            hability["Dash"].Hability();
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
