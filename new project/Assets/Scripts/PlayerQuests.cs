using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public QuestList questList;

    private void Awake()
    {
        questList = new QuestList(5);
    }
}
