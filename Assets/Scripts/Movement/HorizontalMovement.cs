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

        float movement = pl.GetComponent<PlayerInput>().MainHorizontal();
        pl.transform.eulerAngles = movement == 0 ? pl.transform.eulerAngles : new Vector3(0, Mathf.Sign(pl.moveVector.x) * 90, 0);

        pl.moveVector.x = movement * pl.moveSpeed + pl.impactVelocity.x;
        pl.moveVector.y = pl.verticalVelocity;
        pl.moveVector.z = 0;

    }
}
