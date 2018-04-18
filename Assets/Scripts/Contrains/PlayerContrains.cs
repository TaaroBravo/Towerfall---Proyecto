using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrains : MonoBehaviour {

    Transform player;
    float myContrains_Z;

    public float leftX;
    public float rightX;

    float buffer = 1f;

    void Start ()
    {
        player = gameObject.transform;
        myContrains_Z = 0;
	}	

	void Update ()
    {
        if (player.position.z != 0)
            player.position = new Vector3(player.position.x, player.position.y, myContrains_Z);
        if (player.position.x < leftX - buffer)
            OnMyLeft();
        else if (player.position.x > rightX + buffer)
            OnMyRight();
    }

    void OnMyLeft()
    {
        player.position = new Vector3(rightX, player.position.y, 0);
    }

    void OnMyRight()
    {
        player.position = new Vector3(leftX, player.position.y, 0);
    }
}
