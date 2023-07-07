using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForLoopPuzzle : WallPuzzle
{
    public int cs1010Progress;

    private int randX;
    private int randA;
    private int randB;
    public int puzzleAnswer;

    protected override void Start()
    {
        base.Start();
        randX = Random.Range(6, 15);
        randA = Random.Range(5, 10);
        randB = Random.Range(2, 5);
    }

    protected override void ChangePuzzleText()
    {
        puzzleText.text = "Solve the for loop to free yourself!" +
            "\nEvery wrong answer has consequences..." +
            "\n\nint x = " + randX + ";" +
            "\nfor (int i = 0; i < " + randA + "; i++)" +
            "\n{" +
            "\n    x = x * " + randB + " + i;" +
            "\n}" +
            "\n\nWhat is the value of x after the for loop?";
    }

    protected override int GetPuzzleAnswer()
    {
        puzzleAnswer = randX;
        int a = randA;
        int b = randB;
        for (int i = 0; i < a; i++)
        {
            puzzleAnswer = puzzleAnswer * b + i;
        }
        return puzzleAnswer;
    }

    protected override void SpawnEnemy()
    {
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)0.9, (float)-2.2), Quaternion.identity);
        Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(2, (float)-2.2), Quaternion.identity);
    }

    public override bool CheckInBattle()
    {
        if (GameObject.Find("Slime(Clone)") == null)
        {
            inBattle = false;
            startBattle = false;
            return false;
        }
        return true;
    }

    protected override void ChangeQuestProgress()
    {
        playerQuests.cs1010Progress = cs1010Progress;
        puzzleCorrect.SetActive(true);
    }

    public override void CheckQuestProgress(int questProgress)
    {
        if (questProgress >= cs1010Progress)
        {
            puzzleCorrect.SetActive(true);
        }
    }
}
