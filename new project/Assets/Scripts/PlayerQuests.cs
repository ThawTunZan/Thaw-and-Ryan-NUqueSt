using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerQuests : MonoBehaviour, IDataPersistence
{
    public QuestList questList;
    public PlayerPositionSO startingPosition;
    public SpriteRenderer playerRenderer;

    private void Start()
    {
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            questList = new QuestList(5);
            questList = GameManager.instance.questList;
        }
    }

    private void Update()
    {
        GameManager.instance.questList = questList;
    }

    public void LoadData(GameData data)
    {
        questList = new QuestList(5);
        questList = data.questList;
    }

    public void SaveData(GameData data)
    {
        data.questList = questList;
    }
}
