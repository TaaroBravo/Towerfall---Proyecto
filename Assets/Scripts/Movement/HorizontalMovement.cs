using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : IMove {

    public void Move(PlayerController pl)
    {
        if (pl.controller.isGrounded)
            pl.verticalVelocity = -pl.gravity * Time.deltaTime;
        else
            pl.verticalVelocity -= pl.gravity * Time.deltaTime;

        pl.moveVector.x = pl.GetComponent<PlayerInput>().MainHorizontal() * pl.moveSpeed + pl.impactVelocity.x;
        pl.moveVector.y = pl.verticalVelocity;
        pl.moveVector.z = 0;

    }
}
