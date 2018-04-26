using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public GameObject prefabPowerUp;
    public List<Transform> powerupsPos;
    public float spawnTimer;
	
	void Update ()
    {
        spawnTimer += Time.deltaTime;
		if(spawnTimer > 10f)
        {
            CreatePowerUp();
            spawnTimer = 0;
        }
	}

    void CreatePowerUp()
    {
        var powerUp = Instantiate(prefabPowerUp);
        powerUp.transform.position = powerupsPos[Random.Range(0, powerupsPos.Count - 1)].position;
    }
}
