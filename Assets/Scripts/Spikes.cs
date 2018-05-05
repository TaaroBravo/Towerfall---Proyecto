using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public float lifeIfHits;

    private void Start()
    {
        lifeIfHits = 0.5f;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.GetComponent<PlayerController>())
        {
            PlayerController player = collider.transform.GetComponent<PlayerController>();
            if (player.stunned)
            {
                player.UpdateMyLife(lifeIfHits);
                player.SmoothHitRefleject();
                player.stunned = false;
            }
        }
    }
}
