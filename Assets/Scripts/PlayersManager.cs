using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayersManager : MonoBehaviour
{

    public List<PlayerController> myPlayers = new List<PlayerController>();
    public GameObject phantomPrefab;

    void Update()
    {
        myPlayers = myPlayers.Where(x => x != null && x.GetComponent<PlayerController>()).ToList();
        if (myPlayers.Count <= 1)
            GameObject.Find("GameManager").GetComponent<GameManager>().FinishGame();

        //var deathPlayers = myPlayers.Where(x => x.isDead);
        //if (deathPlayers.Any())
        //{
        //    foreach (var item in deathPlayers)
        //    {
        //        if (!GameObject.Find("Fantasma " + item.gameObject.name))
        //        {
        //            var dead = Instantiate(phantomPrefab);
        //            dead.transform.position = item.transform.position;
        //            dead.GetComponent<PlayerInput>().id = item.GetComponent<PlayerInput>().id;
        //            dead.GetComponent<PlayerInput>().controller = item.GetComponent<PlayerInput>().controller;
        //            dead.name = "Fantasma " + item.gameObject.name;
        //        }
        //    }
        //    deathPlayers = Enumerable.Empty<PlayerController>();
        //}
    }
}
