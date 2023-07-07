using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPuzzleTrigger : MonoBehaviour
{
    public GameObject wallPuzzleTrigger;
    public GameObject wallPuzzleActivate;
    public GameObject lockedWall;

    public string questName;
    public int questProgress;
    public string puzzleType;

    public bool finishBattle;

    bool playerInRange;

    void Update()
    {
        if (playerInRange || finishBattle)
        {
            WallTrigger();
        }
    }

    private void WallTrigger()
    {
        if (questName == "CS1010" && puzzleType == "ForLoop" 
            && GameManager.instance.cs1010Progress < questProgress)
        {
            ActivatePuzzle();
        }
        else if (questName == "CS1231" && (puzzleType == "ImplicationLogic" || puzzleType == "IfAndOnlyIfLogic")
            && GameManager.instance.cs1231Progress < questProgress)
        {
            ActivatePuzzle();
        }
    }

    private void ActivatePuzzle()
    {
        lockedWall.SetActive(true);
        wallPuzzleActivate.SetActive(true);
        finishBattle = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
