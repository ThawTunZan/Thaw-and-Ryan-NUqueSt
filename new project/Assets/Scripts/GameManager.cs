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

    public int ma1511Progress;
    public int eg1311Progress;
    public int cg2111aProgress;

    public List<string> completedQuestNames = new List<string>();
    public List<string> completedQuestDescs = new List<string>();

    public float rockDayChecker;
    public List<string> listOfRockSceneNames = new List<string>();
    public List<List<string>> listOfRockNames = new List<List<string>>();
    public List<List<int>> listOfRockStates = new List<List<int>>();

    public float enemyDayChecker;
    public List<string> listOfEnemySceneNames = new List<string>();
    public List<List<string>> listOfEnemyNames = new List<List<string>>();
    public List<List<int>> listOfEnemyStates = new List<List<int>>();

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

        DontDestroyOnLoad(gameObject);  //else
    }
}
