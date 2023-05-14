using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{

    public Vector3 playerPosition;
    //public string name;
    public int gameScene;

    
    //default value;
    public GameData()
    {
        playerPosition = Vector3.zero;
        gameScene = 0;
    }
}
