using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;
    public static GameManager instance;
    public int level;
    public float exp;

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

    public int money;

    public int tutorialProgress;

    public int cs1010Progress;
    public int cs1231Progress;
    public int cs2030Progress;
    public int cs2040Progress;
    public List<Vector2Int> cs2040SeenBefore = new List<Vector2Int>();

    public float dayChecker;
    public List<List<string>> listOfRockNames = new List<List<string>>();
    public List<string> cave1RockNames = new List<string>();
    public List<string> cave1aRockNames = new List<string>();
    public List<string> cave1bRockNames = new List<string>();
    public List<string> cave2aRockNames = new List<string>();
    public List<string> cave3aRockNames = new List<string>();
    public List<string> cave4aRockNames = new List<string>();
    public List<List<int>> listOfRockStates = new List<List<int>>();
    public List<int> cave1RockStates = new List<int>();
    public List<int> cave1aRockStates = new List<int>();
    public List<int> cave1bRockStates = new List<int>();
    public List<int> cave2aRockStates = new List<int>();
    public List<int> cave3aRockStates = new List<int>();
    public List<int> cave4aRockStates = new List<int>();

    // for day and night system
    public float hours;
    public float minutes;
    public float seconds;
    public float day;

    public string story;

    public List<Vector3Int> seedPositions = new List<Vector3Int>();
    public List<string> seedNames = new List<string>();
    public List<float> seedNextGrowths = new List<float>();

    void Awake()
    { 
        if (instance != null)
        {
            //Debug.LogError("Found more than one GameManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        instance.questList = new QuestList(5);

        instance.inventory = new Inventory("Inventory", 21);
        instance.toolbar = new Inventory("Toolbar", 7);

        instance.chest0 = new Inventory("Chest0", 21);
        instance.chest1 = new Inventory("Chest1", 21);
        instance.chest2 = new Inventory("Chest2", 21);
        chestList.Add(instance.chest0);
        chestList.Add(instance.chest1);
        chestList.Add(instance.chest2);

        shop0 = new Inventory("Shop0", 21);
        shop1 = new Inventory("Shop1", 21);
        shopList.Add(instance.shop0);
        shopList.Add(instance.shop1);

        listOfRockNames.Add(cave1RockNames);
        listOfRockNames.Add(cave1aRockNames);
        listOfRockNames.Add(cave1bRockNames);
        listOfRockNames.Add(cave2aRockNames);
        listOfRockNames.Add(cave3aRockNames);
        listOfRockNames.Add(cave4aRockNames);

        listOfRockStates.Add(cave1RockStates);
        listOfRockStates.Add(cave1aRockStates);
        listOfRockStates.Add(cave1bRockStates);
        listOfRockStates.Add(cave2aRockStates);
        listOfRockStates.Add(cave3aRockStates);
        listOfRockStates.Add(cave4aRockStates);

        DontDestroyOnLoad(gameObject);  //else
    }
}
