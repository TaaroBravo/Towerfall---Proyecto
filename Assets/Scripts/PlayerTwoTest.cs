using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoTest : MonoBehaviour
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
    public Vector3 residualVelocity;

    public float impactVelocity = 20;
    public float impactDistance;
    public float impactMaxTimer;
    public float impactTimerF;
    public bool receivingDamage;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        impactDistance = 15;
        impactMaxTimer = impactDistance / impactVelocity;
    }

    void Update()
    {
        Move();
        controller.Move(moveVector * Time.deltaTime);

        if(receivingDamage)
        {
            impactTimerF += Time.deltaTime;
            if (impactTimerF > impactMaxTimer)
            {
                receivingDamage = false;
                impactTimerF = 0;
                residualVelocity = Vector3.zero;
            }
        }

    }

    public void ReceiveDamage(Vector3 impact)
    {
        residualVelocity = impact;
        receivingDamage = true;
        impactTimerF = 0;
    }

    void Move()
    {
        if(receivingDamage)
        {
            moveVector.x = residualVelocity.x;
            moveVector.y = residualVelocity.y;
            moveVector.z = 0;
        }
        else
        {
            if (controller.isGrounded)
                verticalVelocity = -gravity * Time.deltaTime;
            else
                verticalVelocity -= gravity * Time.deltaTime;

            moveVector.x = 0;
            moveVector.y = verticalVelocity;
            moveVector.z = 0;
        }

        //residualVelocity = new Vector3(residualVelocity.x, residualVelocity.y + verticalVelocity);
        //residualVelocity -= residualVelocity / 2 * Time.deltaTime;

        //moveVector.x = residualVelocity.x;
        //moveVector.y = residualVelocity.y;
        //moveVector.z = 0;
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
