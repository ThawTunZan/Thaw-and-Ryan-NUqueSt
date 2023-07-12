using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRocks : MonoBehaviour, IDataPersistence
{
    public List<List<string>> listOfRockNames = new List<List<string>>();
    public List<List<int>> listOfRockStates = new List<List<int>>();

    private float dayChecker;

    public PlayerPositionSO startingPosition;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            listOfRockNames = GameManager.instance.listOfRockNames;
            listOfRockStates = GameManager.instance.listOfRockStates;
            dayChecker = GameManager.instance.dayChecker;
        }
        string currScene = SceneManager.GetActiveScene().name;
        CheckNewDay();
        CheckCaveScene(currScene);
    }

    private void Update()
    {
        GameManager.instance.dayChecker = dayChecker;
        GameManager.instance.listOfRockNames = listOfRockNames;
        GameManager.instance.listOfRockStates = listOfRockStates;
    }

    private void CheckNewDay()
    {
        if (GameManager.instance.day > dayChecker)
        {
            for (int i = 0; i < listOfRockNames.Count; i++)
            {
                if (listOfRockNames[i].Count != 0)
                {
                    for (int j = 0; j < listOfRockStates[i].Count; j++)
                    {
                        listOfRockStates[i][j] = 1;
                    }
                }
            }
            dayChecker = GameManager.instance.day;
        }
    }

    private void CheckCaveScene(string currScene)
    {
        if (currScene == "Cave_1")
        {
            UpdateCaveList(0);
        }
        else if (currScene == "Cave_1a")
        {
            UpdateCaveList(1);
        }
        else if (currScene == "Cave_1b")
        {
            UpdateCaveList(2);
        }
        else if (currScene == "Cave_2a")
        {
            UpdateCaveList(3);
        }
        else if (currScene == "Cave_3a")
        {
            UpdateCaveList(4);
        }
        else if (currScene == "Cave_4a")
        {
            UpdateCaveList(5);
        }
    }

    private void UpdateCaveList(int index)
    {
        if (listOfRockNames[index].Count == 0)
        {
            Transform rockSpawner = GameObject.Find("RockSpawner").transform;
            foreach (Transform child in rockSpawner)
            {
                listOfRockNames[index].Add(child.gameObject.name);
                listOfRockStates[index].Add(1);
            }
        }
        else
        {
            for (int i = 0; i < listOfRockNames[index].Count; i++)
            {
                int state = listOfRockStates[index][i];
                if (state == 0)
                {
                    GameObject rock = GameObject.Find(listOfRockNames[index][i]);
                    rock.SetActive(false);
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        listOfRockNames = data.listOfRockNames;
        listOfRockStates = data.listOfRockStates;
        dayChecker = data.dayChecker;
    }

    public void SaveData(GameData data)
    {
        data.listOfRockNames = listOfRockNames;
        data.listOfRockStates = listOfRockStates;
        data.dayChecker = dayChecker;
    }
}
