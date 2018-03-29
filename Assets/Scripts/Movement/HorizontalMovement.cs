using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : IMove {

    public void Move(PlayerController pl)
    {
        pl.moveVector = Vector3.zero;
        pl.moveVector.x = Input.GetAxis("Horizontal") * pl.moveSpeed;
        pl.moveVector.y = pl.verticalVelocity;
        pl.moveVector.z = 0;
    }
}
