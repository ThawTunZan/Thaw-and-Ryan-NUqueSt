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

    bool playerInRange;

    void Update()
    {
        if (playerInRange)
        {
            WallTrigger();
        }
    }

    private void WallTrigger()
    {
        if (questName == "CS1010" && puzzleType == "ForLoop" && GameManager.instance.cs1010Progress < questProgress)
        {
            ForLoopPuzzle forLoopPuzzle = wallPuzzleTrigger.GetComponent<ForLoopPuzzle>();
            if (!forLoopPuzzle.CheckInBattle())
            {
                lockedWall.SetActive(true);
                wallPuzzleActivate.SetActive(true);
            }
        }
        else if (questName == "CS1231" && puzzleType == "ImplicationLogic" && GameManager.instance.cs1231Progress < questProgress)
        {
            ImplicationLogicPuzzle logicStatementsPuzzle = wallPuzzleTrigger.GetComponent<ImplicationLogicPuzzle>();
            if (!logicStatementsPuzzle.CheckInBattle())
            {
                lockedWall.SetActive(true);
                wallPuzzleActivate.SetActive(true);
            }
        }
        else if (questName == "CS1231" && puzzleType == "IfAndOnlyIfLogic" && GameManager.instance.cs1231Progress < questProgress)
        {
            IfAndOnlyIfLogicPuzzle logicStatementsPuzzle = wallPuzzleTrigger.GetComponent<IfAndOnlyIfLogicPuzzle>();
            if (!logicStatementsPuzzle.CheckInBattle())
            {
                lockedWall.SetActive(true);
                wallPuzzleActivate.SetActive(true);
            }
        }
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
