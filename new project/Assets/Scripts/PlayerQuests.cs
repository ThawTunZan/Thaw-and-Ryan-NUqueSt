using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuests : MonoBehaviour, IDataPersistence
{
    public QuestList questList;
    public PlayerPositionSO startingPosition;

    public int cs1010Progress;
    public int cs1231Progress;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            questList = new QuestList(5);
            questList = GameManager.instance.questList;

            cs1010Progress = GameManager.instance.cs1010Progress;
            cs1231Progress = GameManager.instance.cs1231Progress;
        }
        string currScene = SceneManager.GetActiveScene().name;
        CheckQuestProgress(currScene);
    }

    /*
     * Certain scenes will look different if specific quests are completed. For example, in Cave_1a, both doors will be opened and
     * the wall puzzle will be solved whenever the player transits to that scene if they have already completed the CS1010 puzzle.
     */
    private void CheckQuestProgress(string currScene)
    {
        if (currScene == "Cave_1a") // For CS1010
        {
            PuzzleDoor puzzle1 = GameObject.Find("PuzzleDoor1").GetComponent<PuzzleDoor>();
            ForLoopPuzzle puzzle2 = GameObject.Find("WallPuzzleTrigger").GetComponent<ForLoopPuzzle>();
            puzzle1.CheckDoorsAtStart(cs1010Progress);
            puzzle2.CheckQuestProgress(cs1010Progress);
        }
        else if (currScene == "Cave_2a") // For CS1231
        {
            ImplicationLogicPuzzle puzzle1 = GameObject.Find("WallPuzzleTrigger").GetComponent<ImplicationLogicPuzzle>();
            IfAndOnlyIfLogicPuzzle puzzle2 = GameObject.Find("WallPuzzleTrigger2").GetComponent<IfAndOnlyIfLogicPuzzle>();
            puzzle1.CheckQuestProgress(cs1231Progress);
            puzzle2.CheckQuestProgress(cs1231Progress);
        }
        else if (currScene == "Cave_3a" && cs1010Progress > 7) // For CS2030
        {

        }
        else if (currScene == "Cave_3a" && cs1010Progress > 10) // For CS2040
        {

        }
    }

    private void Update()
    {
        GameManager.instance.questList = questList;

        GameManager.instance.cs1010Progress = cs1010Progress;
        GameManager.instance.cs1231Progress = cs1231Progress;
    }

    public void LoadData(GameData data)
    {
        questList = new QuestList(5);
        questList = data.questList;

        cs1010Progress = data.cs1010Progress;
        cs1231Progress = data.cs1231Progress;
    }

    public void SaveData(GameData data)
    {
        data.questList = questList;

        data.cs1010Progress = cs1010Progress;
        data.cs1231Progress = cs1231Progress;
    }
}
