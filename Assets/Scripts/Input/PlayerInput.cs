using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public Controller controller;
    public int id;

    PlayerController player;
    public string horizontalMove;
    public string verticalMove;
    public string jumpButton;
    public string normalAttack;
    public string downAttack;
    public string upAttack;
    public string habilityButton;

    public enum Controller
    {
        J,
        K
    }

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
        r += Input.GetAxis(controller.ToString() + "_MainHorizontal_P" + id);
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public void SetPlayerInput()
    {
        horizontalMove = controller.ToString() + "_MainHorizontal_P" + id;
        verticalMove = controller.ToString() + "_MainVertical_P" + id;
        jumpButton = controller.ToString() + "_JumpButton_P" + id;
        normalAttack = controller.ToString() + "_NormalAttack_P" + id;
        downAttack = controller.ToString() + "_DownAttack_P" + id;
        upAttack = controller.ToString() + "_UpAttack_P" + id;
        habilityButton = controller.ToString() + "_Hability_P" + id;
    }
}
