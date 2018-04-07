using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDamage : MonoBehaviour
{

    public enum Direction
    {
        Up,
        Down,
        NegativeHorizontal,
        PositiveHorizontal,
    }

    public Direction myDir;

    void Update()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, 2f))
        {
            if (item.GetComponent<PlayerTwoTest>())
            {
                if (myDir == Direction.Up)
                {
                    item.GetComponent<PlayerTwoTest>().ReceiveDamage(new Vector3(0, 40, 0));
                }
                if (myDir == Direction.Down)
                {
                    item.GetComponent<PlayerTwoTest>().ReceiveDamage(new Vector3(0, -40, 0));
                }
                if (myDir == Direction.NegativeHorizontal)
                {
                    item.GetComponent<PlayerTwoTest>().ReceiveDamage(new Vector3(-40, 0, 0));
                }
                if (myDir == Direction.PositiveHorizontal)
                {
                    item.GetComponent<PlayerTwoTest>().ReceiveDamage(new Vector3(40, 0, 0));
                }
            }
        }
    }
}
