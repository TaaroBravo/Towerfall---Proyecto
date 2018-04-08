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
        weaponExtends = 5;
        impactVelocity = 2;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack(Collider col)
    {
        if (timerCoolDownAttack < 0)
        {
            Debug.Log("Up_J");
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if(CheckParently(c.transform))
                    continue;
                PlayerController target = TargetScript(c.transform);
                if(target != null)
                    target.ReceiveDamage(new Vector3(0, impactVelocity * Mathf.Abs(player.moveVector.x), 0));
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
