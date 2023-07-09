using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour, IDataPersistence
{
    public PlayerPositionSO startingPosition;

    public int tutorialProgress;

    void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            tutorialProgress = GameManager.instance.tutorialProgress;
        }
    }

    void Update()
    {
        GameManager.instance.tutorialProgress = tutorialProgress;
    }

    public void LoadData(GameData data)
    {
        tutorialProgress = data.tutorialProgress;
    }

    public void SaveData(GameData data)
    {
        data.tutorialProgress = tutorialProgress;
    }
}