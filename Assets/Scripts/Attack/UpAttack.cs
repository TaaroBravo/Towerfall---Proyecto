using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAttack : IAttack
{

    public void Attack(Collider col, Transform player)
    {
        Collider[] cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents * 2.2f, col.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            //Cambiar el c.transform.parent si algo cambia de la estructura
            if (c.transform.parent.parent == player.transform)
                continue;
            //Hacer daño
            PlayerTwoTest enemy = c.transform.parent.GetComponent<PlayerTwoTest>();
            enemy.ReceiveDamage("Up");
        }
    }
}
