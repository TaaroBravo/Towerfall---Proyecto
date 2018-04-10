using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IMove {

    public void Move(PlayerController pl)
    {
        pl.verticalVelocity = pl.jumpForce;
        pl.moveVector.y = pl.verticalVelocity;
    }
}
