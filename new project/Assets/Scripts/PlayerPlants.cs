using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerPlants : MonoBehaviour, IDataPersistence
{
    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();
    public PlayerPositionSO startingPosition;

    void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            seedPositions = GameManager.instance.seedPositions;
            seedNames = GameManager.instance.seedNames;
            seedNextGrowths = GameManager.instance.seedNextGrowths;
        }
    }

    void Update()
    {
        GameManager.instance.seedPositions = seedPositions;
        GameManager.instance.seedNames = seedNames;
        GameManager.instance.seedNextGrowths = seedNextGrowths;
        CheckPlantGrowth();
    }

    private void CheckPlantGrowth()
    {
        for (int i = 0;  i < seedPositions.Count; i++)
        {
            if (GameManager.instance.hours == seedNextGrowths[0] && GameManager.instance.minutes == seedNextGrowths[1])
            {
                char seedStage = seedNames[i][seedNames[i].Length - 1];
                if (seedStage != '5')
                {
                    int seedStageInt = (int)seedStage + 1;
                    char newSeedStage = (char)seedStageInt;
                    seedNames[i] = seedNames[i].Substring(0, seedNames[i].Length - 1) + newSeedStage;
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        seedPositions = data.seedPositions;
        seedNames = data.seedNames;
        seedNextGrowths = data.seedNextGrowths;
    }

    public void SaveData(GameData data)
    {
        data.seedPositions = seedPositions;
        data.seedNames = seedNames;
        data.seedNextGrowths = seedNextGrowths;
    }
}
