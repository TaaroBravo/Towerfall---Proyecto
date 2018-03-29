using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IMove {

    public void Move(PlayerController pl)
    {
        pl.isDashing = true;
        pl.moveVector.x = Mathf.Sign(pl.moveVector.x) * pl.dashSpeed;
        pl.verticalVelocity = 0;
        pl.moveVector.y = pl.verticalVelocity;
    }
}
