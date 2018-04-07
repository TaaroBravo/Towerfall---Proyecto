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

    public float normalAttackCoolDown;
    public float upAttackCoolDown;
    public float downAttackCoolDown;

    public float dashCoolDown;
    public bool isDashing;
    public bool canDash = true;

    private IMove _iMove;

    public Collider[] attackColliders;
    private Dictionary<string, IAttack> attacks = new Dictionary<string, IAttack>();
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();

    private Vector3 _residualVelocity;
    public Vector3 residualVelocity
    {
        get
        {
            return _residualVelocity;
        }
        set
        {
            if (_residualVelocity.magnitude < maxVelocity)
                _residualVelocity = value;
            else
                _residualVelocity -= _residualVelocity / 2 * Time.deltaTime;
        }
    }
    public float maxVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        SetAttacks();
        SetHabilities();
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
        foreach (var a in attacks.Values)
            a.Update();

        if (InputManager.XButton())
            attacks["NormalAttack"].Attack(attackColliders[0]);

        if (InputManager.YButton())
            attacks["UpAttack"].Attack(attackColliders[0]);

        if (InputManager.BButton())
            attacks["DownAttack"].Attack(attackColliders[0]);
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

    private void SetAttacks()
    {
        normalAttackCoolDown = 0.25f;
        upAttackCoolDown = 1f;
        downAttackCoolDown = 1f;
        attacks.Add(typeof(NormalAttack).ToString(), new NormalAttack(this, normalAttackCoolDown));
        attacks.Add(typeof(UpAttack).ToString(), new UpAttack(this, upAttackCoolDown));
        attacks.Add(typeof(DownAttack).ToString(), new DownAttack(this, downAttackCoolDown));
    }

    private void SetHabilities()
    {
        dashCoolDown = 3f;
        hability.Add(typeof(Dash).ToString(), new Dash(this, dashCoolDown));
    }
}
