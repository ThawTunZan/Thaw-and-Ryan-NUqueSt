using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueensPuzzle : WallPuzzle
{
    public int cs2040Progress;

    private QueenChecker queenChecker;

    public int currQ = 1;
    private int totalQ = 5;

    [SerializeField] private GameObject puzzleDoor1;

    protected override void Start()
    {
        base.Start();
        queenChecker = GameObject.Find("QueenPieces").GetComponent<QueenChecker>();
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the n-Queens puzzle or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nHold a weapon and left click a queen to move it around." +
            "\nYou can only move the queen left and right." +
            "\n\nIf you think your configuration is correct, click OK." +
            "\nYou have to solve this " + (totalQ - currQ + 1) + " more times!";
    }

    protected override int GetPuzzleAnswer()
    {
        if (queenChecker.CheckX())
        {
            return 0;
        }
        foreach (Vector2Int queenPosition in queenChecker.queenPositions)
        {
            if (queenChecker.CheckDiag(queenPosition))
            {
                return 0;
            }
        }
        return 1;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)0.318, (float)0.148), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)0.486, (float)-0.484), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Skeleton"), new Vector2((float)1.677, (float)-0.322), Quaternion.identity);
    }

    protected override void CheckInBattle()
    {
        if (GameObject.Find("Skeleton(Clone)") == null)
        {
            inBattle = false;
            startBattle = false;
            puzzleTrigger.inBattle = false;
            puzzleTrigger.finishBattle = true;
        }
    }

    protected override void ChangeQuestProgress()
    {
        playerQuests.cs2040Progress = cs2040Progress;
        puzzleCorrect.SetActive(true);
        puzzleTrigger.gameObject.SetActive(false);
        puzzleDoor1.SetActive(false);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs2040Progress)
        {
            puzzleCorrect.SetActive(true);
            puzzleTrigger.gameObject.SetActive(false);
            puzzleDoor1.SetActive(false);
        }
    }

    public override void CheckAnswer()
    {
        if (GetPuzzleAnswer() == 1)
        {
            if (queenChecker.HasSeenBefore())
            {
                puzzleText.text = "You have tried this configuration already.\n\nTry again!";
            }
            else if (currQ == totalQ)
            {
                puzzleText.text = "Correct!\n\nYou are now freed from this room.";
                ChangeQuestProgress();
            }
            else
            {
                currQ++;
                GetPuzzleAnswer();
                ChangePuzzleText();
            }
        }
        else
        {
            puzzleText.text = "Oh no, that is wrong...\n\nThere is a surprise waiting for you :)";
            startBattle = true;
        }
    }
}
