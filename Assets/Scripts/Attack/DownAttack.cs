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
        weaponExtends = 4;
        impactVelocity = 40;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack(Collider col)
    {
        if (timerCoolDownAttack < 0)
        {
            Debug.Log("Down_J");
            Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * weaponExtends, col.transform.rotation, LayerMask.GetMask("Hitbox"));
            foreach (Collider c in cols)
            {
                if (CheckParently(c.transform))
                    continue;
                //Hacer daño
                PlayerController target = TargetScript(c.transform);
                if (target != null)
                    target.ReceiveDamage(new Vector3(0, -impactVelocity, 0));
            }
            timerCoolDownAttack = coolDownAttack;
        }
    }
}
