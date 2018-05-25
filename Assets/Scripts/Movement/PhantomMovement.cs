using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMovement : IMove
{

    PhantomController phantom;
    public PhantomMovement(PhantomController pl)
    {
        phantom = pl;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        float moveHorizontal = phantom.GetComponent<PlayerInput>().MainHorizontal();
        if (moveHorizontal != 0)
        {
            float speed = 360.0f;
            float time = 1.0f;
            phantom.transform.RotateAround(phantom.transform.position, Vector3.forward, speed * moveHorizontal * Time.deltaTime / time);
        }
    }


}
