using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IHability {

    PlayerController player;
    public float dashSpeed = 50;
    public float dashDistance = 7;
    public float dashingTime;
    public float dashTimer;

    public Dash(PlayerController p, float _timerCoolDown = 0)
    {
        player = p;
        timerCoolDown = _timerCoolDown;
        coolDown = _timerCoolDown;
        dashTimer = dashDistance / dashSpeed;
    }

    public override void Update()
    {
        base.Update();
        if (player.isDashing && dashingTime <= dashTimer)
            dashingTime += Time.deltaTime;
        else
        {
            dashingTime = 0;
            player.isDashing = false;
        }

        if (player.controller.isGrounded && !player.isDashing)
            player.canDash = true;

    }

    public override void Hability()
    {
        if(timerCoolDown < 0)
        {
            player.isDashing = true;
            player.canDash = false;
            player.moveVector.x = player.moveVector.x != 0 ? Mathf.Sign(player.moveVector.x) * dashSpeed : dashSpeed;
            player.verticalVelocity = 0;
            player.moveVector.y = player.verticalVelocity;
            timerCoolDown = coolDown;
        }
    }
}
