using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatforms : MonoBehaviour {

    public float lifeOfPlatform;

	void Update ()
    {
        Debug.Log(lifeOfPlatform);
        if (lifeOfPlatform <= 0)
            Destroy(gameObject);
	}

    private void OnDestroy()
    {
        //Feedback
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.GetComponent<PlayerController>())
        {
            PlayerController player = collider.transform.GetComponent<PlayerController>();
            if(player.impactSpeed > 25)
                lifeOfPlatform -= player.impactSpeed;
        }
    }
}
