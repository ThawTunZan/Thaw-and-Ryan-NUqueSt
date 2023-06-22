using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

[System.Serializable]

public class GameData
{

    public Vector3 playerPosition;
    public string name;
    public float maxHealth;

    public QuestList questList;

    public Inventory inventory;
    public Inventory toolbar;

    public Inventory chest0;
    public List<Inventory> chestList = new List<Inventory>();

    public string story;                        // quest progress for WeaponSmith NPC
    public string placeHolderStory; 
    
    public float hours;

    public float day;

    
    //default value;
    public GameData()
    {
        playerPosition = Vector3.zero;
       // gameScene = 3;
        maxHealth = 100;

        questList = new QuestList(5);

        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);

        // chest0 is in PlayerHouse
        chest0 = new Inventory("Chest0", 21) { };

        chestList.Add(chest0);

        hours = 8;

        story = "";
        placeHolderStory = "";

        day = 0;
    }
}
