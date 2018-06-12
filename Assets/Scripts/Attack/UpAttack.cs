using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAttack : IAttack
{
    public UpAttack(PlayerController pl, float _timerCoolDown = 0)
    {
        player = pl;
        timerCoolDownAttack = _timerCoolDown;
        coolDownAttack = _timerCoolDown;
        weaponExtends = player.weaponExtends;
        impactVelocity = player.impactVelocityUp;
        defaultAttack = player.defaultAttackUp;
        influenceOfMovement = player.influenceOfMovementUp;
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
                player.myAnim.Play("AttackUp");
            else
                player.myAnim.Play("HitUpAir");
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if (CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                player.hitParticles.Play();
                //Ver si hay que sacar el Charged y remplazarlo por mark;
                if (target != null && !player.isCharged)
                {
                    target.ReceiveDamage(new Vector3(0, impactVelocity * (Mathf.Abs(player.moveVector.x == 0 ? defaultAttack : player.moveVector.x) / influenceOfMovement), 0));
                    player.whoIHited = target;
                }
                else if (target != null)
                {
                    player.whoIHited = target;
                    chargedEffect = player.chargedEffect;
                    target.ReceiveDamage(new Vector3(0, chargedEffect, 0));
                    target.stunned = true;
                    target.WhoHitedMe(player);
                }
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
