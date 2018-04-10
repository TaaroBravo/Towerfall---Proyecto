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
    //private Dictionary<string, IMove> myMoves = new Dictionary<string, IMove>();
    private Dictionary<string, IAttack> attacks = new Dictionary<string, IAttack>();
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();

    public Vector3 impactVelocity;

    public float impactSpeed = 20;
    public float impactDistance;
    public float impactMaxTimer;
    public float impactTimerF;

    public bool stuned;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        //SetMovements();
        SetAttacks();
        SetHabilities();
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
<<<<<<< HEAD

=======
>>>>>>> 6c7917c4857b81c4425f5b38ea36adc2bbaa2204
        if (stuned)
        {
            moveVector.x = impactVelocity.x;
            moveVector.y = impactVelocity.y;
        }
        else
        {
            foreach (var m in myMoves.Values)
                m.Update();
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
        Vector3 impactRelax = Vector3.zero;
        if (stuned && impactTimerF > 0.1f)
        {
            impactRelax = (impactVelocity.magnitude / 13) * impact;
            impactRelax = new Vector3(impactRelax.x > 70 ? 70 : impactRelax.x, impactRelax.y > 70 ? 70 : impactRelax.y, 0);
            impactVelocity = impactRelax;
            SetImpacts();
        }
        else
        {
            impactVelocity = impact;
            SetImpacts();
            stuned = true;
        }
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
<<<<<<< HEAD
        if (!controller.isGrounded && dir == -1)
=======
        if (!controller.isGrounded && Vector3.Angle(transform.up, hit.normal) == 180)
>>>>>>> 6c7917c4857b81c4425f5b38ea36adc2bbaa2204
        {
            verticalVelocity =  -7;
            moveVector.y = verticalVelocity;
        }
        else if (!controller.isGrounded && Vector3.Angle(transform.up, hit.normal) >= 90)
        {
            Debug.Log(gameObject.name);
            if (stuned)
            {
                moveVector = Vector3.zero;
                verticalVelocity -= gravity * Time.deltaTime * 2;
                stuned = false;
            }

        }
        else
            canJump = false;
    }

    //private void SetMovements()
    //{
    //    myMoves.Add(typeof(HorizontalMovement).ToString(), new HorizontalMovement(this));
    //    myMoves.Add(typeof(Jump).ToString(), new Jump(this));
    //}

    private void SetAttacks()
    {
        normalAttackCoolDown = 0.25f;
        upAttackCoolDown = 0.25f;
        downAttackCoolDown = 0.25f;
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
<<<<<<< HEAD
        impactSpeed = Mathf.Abs(impactVelocity.magnitude);
        impactMaxTimer = impactSpeed / 60;
=======
        impactDistance = 15;
        impactMaxTimer = impactDistance / impactSpeed;
>>>>>>> 6c7917c4857b81c4425f5b38ea36adc2bbaa2204
    }
}
