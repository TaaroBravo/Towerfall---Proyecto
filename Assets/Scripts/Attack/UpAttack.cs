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
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if(CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                player.myAnim.Play("CastSpell");
                player.hitParticles.Play();
                if(target != null && !player.isCharged)
                    target.ReceiveDamage(new Vector3(0, impactVelocity * (Mathf.Abs(player.moveVector.x == 0 ? defaultAttack : player.moveVector.x) / influenceOfMovement), 0));
                else if(target != null)
                {
                    chargedEffect = player.chargedEffect;
                    target.ReceiveDamage(new Vector3(0, chargedEffect, 0));
                    player.hitCharged = true;
                    target.stuned = true;
                }
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
