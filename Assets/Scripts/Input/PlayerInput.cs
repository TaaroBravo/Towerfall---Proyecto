using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public string controller;
    public int id;

    PlayerController player;
    public string horizontalMove;
    public string verticalMove;
    public string jumpButton;
    public string normalAttack;
    public string downAttack;
    public string upAttack;
    public string habilityButton;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        SetPlayerInput();
    }

    void Update ()
    {
        if (Input.GetButtonDown(jumpButton))
            player.canJump = true;
        if (Input.GetButtonDown(normalAttack))
            player.AttackNormal();
        if (Input.GetButtonDown(downAttack))
            player.AttackDown();
        if (Input.GetButtonDown(upAttack))
            player.AttackUp();

    }

    public float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal_P" + id);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public void SetPlayerInput()
    {
        horizontalMove = controller + "_MainHorizontal_P" + id;
        verticalMove = controller + "_MainVertical_P" + id;
        jumpButton = controller + "_JumpButton_P" + id;
        normalAttack = controller + "_NormalAttack_P" + id;
        downAttack = controller + "_DownAttack_P" + id;
        upAttack = controller + "_UpAttack_P" + id;
        habilityButton = controller + "_Hability_P" + id;
    }
}
