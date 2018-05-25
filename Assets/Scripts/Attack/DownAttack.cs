using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAttack : IAttack
{
    public DownAttack(PlayerController pl, float _timerCoolDown = 0)
    {
        player = pl;
        timerCoolDownAttack = _timerCoolDown;
        coolDownAttack = _timerCoolDown;
        weaponExtends = player.weaponExtends;
        impactVelocity = player.impactVelocityDown;
        defaultAttack = player.defaultAttackDown;
        influenceOfMovement = player.influenceOfMovementDown;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack(Collider col)
    {
        if (timerCoolDownAttack < 0)
        {
            player.myAnim.Play("AttackDown");
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if (CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                player.hitParticles.Play();
                //Ver si hay que sacar el Charged;
                if (target != null && !player.isCharged)
                    target.ReceiveDamage(new Vector3(0, -impactVelocity * (Mathf.Abs(player.moveVector.x == 0 ? defaultAttack : player.moveVector.x) / influenceOfMovement), 0));
                else if (target != null)
                {
                    chargedEffect = player.chargedEffect * 3;
                    target.ReceiveDamage(new Vector3(0, -player.chargedEffect, 0));
                    player.marked = true;
                    target.WhoHitedMe(player);
                }
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
