using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public int deadImpact;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            PlayerController pl = other.GetComponent<PlayerController>();
            if(pl.moveVector.magnitude >= deadImpact)
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
