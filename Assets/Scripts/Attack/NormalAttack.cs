using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : IAttack
{
    public NormalAttack(PlayerController pl, float _timerCoolDown = 0)
    {
        player = pl;
        timerCoolDownAttack = _timerCoolDown;
        coolDownAttack = _timerCoolDown;
        weaponExtends = player.weaponExtends;
        impactVelocity = player.impactVelocityNormal;
        defaultAttack = player.defaultAttackNormal;
        influenceOfMovement = player.influenceOfMovementNormal;
        chargedEffect = player.chargedEffect;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack(Collider col)
    {
        if (timerCoolDownAttack < 0)
        {
            if (player.myAnim.GetBool("Grounded"))
                player.myAnim.Play("AttackForward");
            else
                player.myAnim.Play("HitForwardAir");
            
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if (CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                player.hitParticles.Play();
                if (target != null && !player.isCharged)
                {
                    target.ReceiveDamage(new Vector3(Mathf.Sign(player.transform.forward.x) * impactVelocity * (Mathf.Abs(player.moveVector.x == 0 ? defaultAttack : player.moveVector.x) / influenceOfMovement), 0, 0));
                    target.WhoHitedMe(player);
                    player.whoIHited = target;
                }
                else if (target != null)
                {
                    player.whoIHited = target;
                    chargedEffect = player.chargedEffect;
                    target.ReceiveDamage(new Vector3(Mathf.Sign(player.transform.forward.x) * chargedEffect, 0, 0));
                    target.WhoHitedMe(player);
                }
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
