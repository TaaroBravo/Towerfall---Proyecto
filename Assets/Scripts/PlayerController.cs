using System;
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

    public bool canJump;

    private IMove _iMove;

    public Collider[] attackColliders;
    private Dictionary<string, IAttack> attacks = new Dictionary<string, IAttack>();
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();

    public Vector3 impactVelocity;

    public float impactSpeed = 20; //En realidad 40?
    public float impactDistance;
    public float impactMaxTimer;
    public float impactTimerF;

    public bool stuned;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        SetAttacks();
        SetHabilities();
        SetImpacts();
    }

    void Update()
    {
        Move();
        Habilities();
        Attack();
        if (stuned)
            StunUpdate();
        controller.Move(moveVector * Time.deltaTime);
    }

    public void Move()
    {
        if (stuned)
        {
            moveVector.x = impactVelocity.x;
            moveVector.y = impactVelocity.y;
        }
        else
        {
            if (canJump && controller.isGrounded)
            {
                _iMove = new Jump();
                _iMove.Move(this);
                canJump = false;
            }
            else if (!isDashing)
            {
                _iMove = new HorizontalMovement();
                _iMove.Move(this);
            }
        }
    }

    public void AttackNormal()
    {
        Debug.Log(2);
        attacks["NormalAttack"].Attack(attackColliders[0]);
    }

    public void AttackDown()
    {
        attacks["DownAttack"].Attack(attackColliders[0]);
    }

    public void AttackUp()
    {
        attacks["UpAttack"].Attack(attackColliders[0]);
    }

    void Habilities()
    {
        foreach (var h in hability.Values)
            h.Update();

        //if (InputManager.LBButton() && canDash)
        //    hability["Dash"].Hability();
    }

    void Attack()
    {
        foreach (var a in attacks.Values)
            a.Update();
    }

    public void ReceiveDamage(Vector3 impact)
    {
        impactVelocity = impact;
        stuned = true;
        impactTimerF = 0;
    }

    private void StunUpdate()
    {
        impactTimerF += Time.deltaTime;
        if (impactTimerF > impactMaxTimer)
        {
            stuned = false;
            impactTimerF = 0;
            impactVelocity = Vector3.zero;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var dir = Vector3.Dot(transform.up, hit.normal);
        if (dir == -1)
        {
            if (verticalVelocity != 0)
                verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else if (dir == 0)
            stuned = false;
        else
            canJump = false;
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

    private void SetImpacts()
    {
        impactDistance = 15;
        impactMaxTimer = impactDistance / impactSpeed;
    }
}
