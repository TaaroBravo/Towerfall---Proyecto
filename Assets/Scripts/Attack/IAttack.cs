using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void Attack(Collider col, Transform player);
}
