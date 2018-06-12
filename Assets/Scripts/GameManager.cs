using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject youWin;

    public void FinishGame()
    {
        youWin.SetActive(true);
        Time.timeScale = 0;
    }
}
