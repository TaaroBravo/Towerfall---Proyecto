using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float myLife;
    public CharacterController controller;

    #region Movement Variables
    public Vector3 moveVector;
    public float moveSpeed;
    public float slowSpeedCharge;
    public float maxSpeedChargeTimer;
    public float verticalVelocity;

    public float gravity;

    public float jumpForce = 20;
    public bool canJump;

    public bool isFalling;
    public float fallOffSpeed;
    #endregion

    #region Attack Variables
    public float weaponExtends;

    public float impactVelocityNormal;
    public float defaultAttackNormal;
    public float normalAttackCoolDown;

    public float impactVelocityUp;
    public float defaultAttackUp;
    public float upAttackCoolDown;

    public float impactVelocityDown;
    public float defaultAttackDown;
    public float downAttackCoolDown;
    #endregion

    #region Dash Variables
    public float dashSpeed = 50;
    public float dashDistance = 7;
    public float dashCoolDown;
    public bool isDashing;
    public bool canDash = true;
    #endregion

    #region ImpactVariables
    public Vector3 impactVelocity;

    public float impactSpeed = 20;
    public float maxImpactToInfinitStun;
    public float impactStunMaxTimer;
    public float currentImpactStunTimer;
    public float residualStunImpact;
    public float influenceOfMovement;

    public float hitHeadReject;
    public float maxNoStunVelocityLimit;
    public float maxStunVelocityLimit;

    public ParticleSystem hitParticles;
    #endregion

    private IMove _iMove;

    public Collider[] attackColliders;

    #region Dictionarys
    private Dictionary<string, IMove> myMoves = new Dictionary<string, IMove>();
    private Dictionary<string, IAttack> attacks = new Dictionary<string, IAttack>();
    private Dictionary<string, IHability> hability = new Dictionary<string, IHability>();
    #endregion

    public bool stuned;

    public float chargedEffect;
    public bool isCharged;
    public bool hitCharged;

	public LifeUI myLifeUI;

    public ParticleSystem PS_Charged;
    public ParticleSystem PS_Fall;
    public Animator myAnim;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetMovements();
        SetAttacks();
        SetHabilities();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        UpdateHabilities();
        Attack();
        if (isCharged)
            Charged();
        else if(PS_Charged.isPlaying)
                PS_Charged.Stop();
        if (stuned)
            StunUpdate();
        controller.Move(moveVector * Time.deltaTime);

		if (myLife <= 0)
			Destroy (gameObject);
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
            foreach (var m in myMoves.Values)
                m.Update();
            if (canJump && controller.isGrounded && !isFalling)
            {
                myMoves["Jump"].Move();
                canJump = false;
            }
            else if (!isDashing && !isFalling)
                myMoves["HorizontalMovement"].Move();
        }
    }

    void Attack()
    {
        foreach (var a in attacks.Values)
            a.Update();
    }

    public void AttackNormal()
    {
        if (!stuned && currentImpactStunTimer < 0.1f)
            attacks["NormalAttack"].Attack(attackColliders[0]);
    }

    public void AttackDown()
    {
        if (!stuned && currentImpactStunTimer < 0.1f)
            attacks["DownAttack"].Attack(attackColliders[0]);
    }

    public void AttackUp()
    {
        if (!stuned && currentImpactStunTimer < 0.1f)
            attacks["UpAttack"].Attack(attackColliders[0]);
    }

    void UpdateHabilities()
    {
        foreach (var h in hability.Values)
            h.Update();
    }

    public void Dash()
    {
        if (canDash)
            hability["Dash"].Hability();
    }

    public void FallOff()
    {
        if (!controller.isGrounded && !stuned && !isDashing)
        {
            myMoves["FallOff"].Move();
            isFalling = true;
        }
    }

    void Charged()
    {
        if (!PS_Charged.isPlaying)
            PS_Charged.Play();
    }

    private void LateUpdate()
    {
        if (hitCharged)
        {
            isCharged = false;
            hitCharged = false;
        }
    }

    public void ReceiveDamage(Vector3 impact)
    {
        Vector3 impactRelax = Vector3.zero;
        if (stuned && currentImpactStunTimer > 0.1f)
        {
            impactRelax = (impactVelocity.magnitude / residualStunImpact) * impact;
            impactRelax = new Vector3(Mathf.Abs(impactRelax.x) > maxStunVelocityLimit ? Mathf.Sign(impactRelax.x) * maxStunVelocityLimit : impactRelax.x, Mathf.Abs(impactRelax.y) > maxStunVelocityLimit ? Mathf.Sign(impactRelax.y) * maxStunVelocityLimit : impactRelax.y, 0);
            impactVelocity = impactRelax;
            SetImpacts();
        }
        else
        {
            impactRelax = impact;
            if(impact.magnitude >= maxNoStunVelocityLimit)
                impactRelax = new Vector3(Mathf.Abs(impactRelax.x) > 0 ? Mathf.Sign(impactRelax.x) * maxNoStunVelocityLimit : 0, Mathf.Abs(impactRelax.y) > 0 ? Mathf.Sign(impactRelax.y) * maxNoStunVelocityLimit : 0, 0);
            impactVelocity = impactRelax;
            SetImpacts();
            stuned = true;
        }
        myAnim.Play("TakeDamage");
        currentImpactStunTimer = 0;
    }

    private void StunUpdate()
    {
        currentImpactStunTimer += Time.deltaTime;
        if (currentImpactStunTimer > impactStunMaxTimer)
        {
            stuned = false;
            currentImpactStunTimer = 0;
            impactVelocity = Vector3.zero;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.gameObject.tag == "Destroyable")
            hit.transform.SendMessage("DestroyablePlatform", this);

        var dir = Vector3.Dot(transform.up, hit.normal);
        if (!controller.isGrounded && dir == -1)
        {
            if(!stuned)
            {
                verticalVelocity = -hitHeadReject;
                moveVector.y = verticalVelocity;
            }
            else
            {
                myLife -= moveVector.magnitude / 15;
                myLifeUI.UpdateMyLife(moveVector.magnitude / 150);
                verticalVelocity = -hitHeadReject;
                moveVector.y = verticalVelocity;
                stuned = false;
            }
        }
        else if (Vector3.Angle(transform.up, hit.normal) >= 90)
        {
            if (stuned)
            {
				myLife -= moveVector.magnitude / 15;
				myLifeUI.UpdateMyLife(moveVector.magnitude / 150);
                moveVector = Vector3.zero;
                verticalVelocity -= gravity * Time.deltaTime * 2;
                impactVelocity.x = Mathf.Sign(moveVector.x) * 3f;
                impactVelocity.y = verticalVelocity;
                stuned = false;
            }
        }
        else
        {
            canJump = false;
            if (stuned && impactVelocity.y > 0)
            {
                myLife -= impactSpeed / 15;
                myLifeUI.UpdateMyLife(impactSpeed / 150);
                impactVelocity = Vector3.zero;
                moveVector = Vector3.zero;
                stuned = false;
            }
            else if(!stuned)
                impactVelocity = Vector3.zero;
            if (isFalling)
            {
                PS_Fall.Play();
                isFalling = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PowerUp"))
        {
            chargedEffect = other.GetComponent<IPowerUp>().PowerUp();
            isCharged = true;
            Destroy(other.gameObject);
        }
    }

    private void SetMovements()
    {
        myMoves.Add(typeof(HorizontalMovement).ToString(), new HorizontalMovement(this));
        myMoves.Add(typeof(Jump).ToString(), new Jump(this));
        myMoves.Add(typeof(FallOff).ToString(), new FallOff(this));
    }

    private void SetAttacks()
    {
        attacks.Add(typeof(NormalAttack).ToString(), new NormalAttack(this, normalAttackCoolDown));
        attacks.Add(typeof(UpAttack).ToString(), new UpAttack(this, upAttackCoolDown));
        attacks.Add(typeof(DownAttack).ToString(), new DownAttack(this, downAttackCoolDown));
    }

    private void SetHabilities()
    {
        hability.Add(typeof(Dash).ToString(), new Dash(this, dashCoolDown));
    }

    private void SetImpacts()
    {
        impactSpeed = Mathf.Abs(impactVelocity.magnitude);
        if (impactSpeed >= maxImpactToInfinitStun)
            impactStunMaxTimer = 100;
        else
            impactStunMaxTimer = impactSpeed / maxImpactToInfinitStun;
    }
}
