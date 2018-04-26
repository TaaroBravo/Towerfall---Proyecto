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
            if (player.stuned)
            {
                player.myLife -= lifeIfHits;
                player.myLifeUI.UpdateMyLife(lifeIfHits);
                player.moveVector = Vector3.zero;
                player.verticalVelocity -= player.gravity * Time.deltaTime * 2;
                player.impactVelocity.x = Mathf.Sign(player.moveVector.x) * 3f;
                player.impactVelocity.y = player.verticalVelocity;
                player.stuned = false;
                Debug.Log("Hola");
            }
        }
    }
}
