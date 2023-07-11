using Ink.Parsed;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UIElements;

[System.Serializable]

public class GameData
{

    public Vector3 playerPosition;
    public string name;
    public float maxHealth;

    public QuestList questList;

    public Inventory inventory;
    public Inventory toolbar;

    public Inventory chest0; // chest0: PlayerHouse
    public Inventory chest1; // chest1: UNUSED
    public Inventory chest2; // chest2: Cave_1
    public List<Inventory> chestList = new List<Inventory>();
    public bool hasAddedToChest;

    public Inventory shop0; // shop0: blacksmith
    public Inventory shop1; // shop1: generalshop
    public List<Inventory> shopList = new List<Inventory>();
    public bool hasAddedToShop;

    public int money;

    public string story;                        // quest progress for WeaponSmith NPC
    public string placeHolderStory;

    // to indicate that a tutorial in a scene is done. Destroys tutorial UI and some colliders if bool is true.
    public int tutorialProgress;

    public int cs1010Progress;
    public int cs1231Progress;
    public int cs2030Progress;
    public int cs2040Progress;
    public List<Vector2Int> cs2040SeenBefore = new List<Vector2Int>();

    public float hours;

    public float day;

    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();

    //default value;
    public GameData()
    {
        playerPosition = Vector3.zero;
       // gameScene = 3;
        maxHealth = 100;

        questList = new QuestList(5);

        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);

        chest0 = new Inventory("Chest0", 21);
        chest1 = new Inventory("Chest1", 21);
        chest2 = new Inventory("Chest2", 21);
        chestList.Add(chest0);
        chestList.Add(chest1);
        chestList.Add(chest2);
        hasAddedToChest = false;

        shop0 = new Inventory("Shop0", 21);
        shop1 = new Inventory("Shop1", 21);
        shopList.Add(shop0);
        shopList.Add(shop1);
        hasAddedToShop = false;

        hours = 8;

        story = "";
        placeHolderStory = "";

        cs1010Progress = -1;
        cs1231Progress = -1;
        cs2030Progress = -1;
        cs2040Progress = -1;

        tutorialProgress = 0;

        day = 0;
    }
}
