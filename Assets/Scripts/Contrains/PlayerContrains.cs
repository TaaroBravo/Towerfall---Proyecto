using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrains : MonoBehaviour {

    Transform player;
    float myContrains_Z;

	void Start ()
    {
        player = gameObject.transform;
        myContrains_Z = 0;
	}	

	void Update ()
    {
        if (player.position.z != 0)
            player.position = new Vector3(player.position.x, player.position.y, myContrains_Z);
	}
}
