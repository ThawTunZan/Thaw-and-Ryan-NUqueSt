using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuests : MonoBehaviour, IDataPersistence
{
    public QuestList questList;
    public PlayerPositionSO startingPosition;

    private string currScene;
    private int cs1010Progress;

    private void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            questList = new QuestList(5);
            questList = GameManager.instance.questList;

            cs1010Progress = GameManager.instance.cs1010Progress;
        }
        CheckCS1010Progress(currScene, cs1010Progress);
    }

    private void CheckCS1010Progress(string currScene, int cs1010Progress)
    {
        if (currScene == "Cave_1a" && cs1010Progress > 0)
        {
            Debug.Log("Here");
        }
        else if (currScene == "Cave_2a" && cs1010Progress > 1)
        {

        }
        else if (currScene == "Cave_3a" && cs1010Progress > 2)
        {

        }
        else if (currScene == "Cave_4a" && cs1010Progress > 3)
        {

        }
    }

    private void Update()
    {
        GameManager.instance.questList = questList;

        GameManager.instance.cs1010Progress = cs1010Progress;
    }

    public void LoadData(GameData data)
    {
        questList = new QuestList(5);
        questList = data.questList;

        cs1010Progress = data.cs1010Progress;
    }

    public void SaveData(GameData data)
    {
        data.questList = questList;

        data.cs1010Progress = cs1010Progress;
    }
}
