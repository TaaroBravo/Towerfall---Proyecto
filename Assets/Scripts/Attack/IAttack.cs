using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IAttack
{
    public PlayerController player;
    public float timerCoolDownAttack;
    public float coolDownAttack;
    //Este valor va a cambiar con el tema de las animaciones y dependiendo el heroe y el arma.
    public float weaponExtends;
    public float impactVelocity;

    public abstract void Attack(Collider col);
    public virtual void Update()
    {
        timerCoolDownAttack -= Time.deltaTime;
    }

    public bool CheckParently(Transform t)
    {
        if (t.parent == null)
            return false;
        if (t.parent == player.transform)
            return true;
        return CheckParently(t.parent);
    }

    public PlayerTwoTest TargetScript(Transform t)
    {
        if (t.GetComponent<PlayerTwoTest>())
            return t.GetComponent<PlayerTwoTest>();
        if (t.parent == null)
            return null;
        return TargetScript(t.parent);
    }
}
