using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerPlants : MonoBehaviour, IDataPersistence
{
    public Dictionary<Vector3Int, List<string>> seedPositionToName = new();
    //public List<Vector3Int> seedPosition = new List<Vector3Int>();
    //public List<string> seedName = new List<string>();
    //public List<int> seedNextGrowth = new List<int>();
    public PlayerPositionSO startingPosition;

    void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            seedPositionToName = GameManager.instance.seedPositionToName;
            //seedPosition = GameManager.instance.seedPosition;
            //seedName = GameManager.instance.seedName;
            //seedNextGrowth = GameManager.instance.seedNextGrowth;
        }
    }

    void Update()
    {
        //GameManager.instance.seedPosition = seedPosition;
        //GameManager.instance.seedName = seedName;
        //GameManager.instance.seedNextGrowth = seedNextGrowth;
        GameManager.instance.seedPositionToName = seedPositionToName;
    }

    public void LoadData(GameData data)
    {
        //seedPosition = data.seedPosition;
        //seedName = data.seedName;
        //seedNextGrowth = data.seedNextGrowth;
        seedPositionToName = data.seedPositionToName;
    }

    public void SaveData(GameData data)
    {
        data.seedPositionToName = seedPositionToName;
        //data.seedPosition = seedPosition;
        //data.seedName = seedName;
        //data.seedNextGrowth = seedNextGrowth;
    }
}
