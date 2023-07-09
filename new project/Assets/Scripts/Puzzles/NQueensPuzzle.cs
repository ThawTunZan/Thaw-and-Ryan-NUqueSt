using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NQueensPuzzle : WallPuzzle
{
    public int cs2040Progress;

    public int puzzleAnswer;

    private int currQ = 1;
    private int totalQ = 5;

    [SerializeField] private GameObject puzzleDoor1;

    protected override void Start()
    {
        base.Start();
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "(" + currQ + "/" + totalQ + ") Solve the n-Queens puzzle or die!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nHold a weapon and left click a queen to move it around." +
            "\nYou can only move the queen left and right." +
            "\nIf you think your board configuration is correct, click OK.";
    }

    protected override int GetPuzzleAnswer()
    {
        return puzzleAnswer;
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
        bool parseSuccess = int.TryParse(puzzleInput.text.Trim(), out int playerAnswer);
        if (parseSuccess)
        {
            if (playerAnswer == GetPuzzleAnswer())
            {
                if (currQ == totalQ)
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
}
