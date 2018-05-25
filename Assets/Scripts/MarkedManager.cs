using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class MarkedManager : MonoBehaviour
{

    public List<PlayerController> allPlayers;
    public PlayerController playerMarked;
    public PlayerController lastPlayerMarked;

    public GameObject markElementPrefab;
    public GameObject markElementPresent;

    void LateUpdate()
    {
        var playerHitted = allPlayers.Where(x => x.countHitByMarked > 3).FirstOrDefault();
        playerMarked = allPlayers.Where(x => x.marked).FirstOrDefault();
        if (playerHitted && playerMarked)
        {
            playerMarked.marked = false;
            allPlayers.Select(x => x.countHitByMarked = 0);
            playerHitted.marked = true;
            playerMarked = playerHitted;
        }

        if (!playerMarked && !markElementPresent)
            ResetMark();
        else
        {
            lastPlayerMarked = playerMarked;
        }
    }

    void ResetMark()
    {
        var r = Random.Range(0, allPlayers.Count - 1);
        PlayerController pl = null;
        allPlayers.Select(x => x.countHitByMarked = 0);
        playerMarked = allPlayers.Where(x => x != lastPlayerMarked).Aggregate(Tuple.Create(pl, 0), (x, y) =>
        {
            if (x.Item2 == r)
                return Tuple.Create(y, x.Item2 + 1);
            else
                return Tuple.Create(x.Item1, x.Item2 + 1);
        }).Item1;
        markElementPresent = Instantiate(markElementPrefab);
        markElementPresent.GetComponent<MarkElement>().player = playerMarked;
    }
}
