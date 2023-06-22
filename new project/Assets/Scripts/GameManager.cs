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

    public Inventory chest0;
    public Inventory chest1;
    public Inventory chest2;
    public List<Inventory> chestList = new List<Inventory>();

    // for day and night system
    public float hours;
    public float minutes;
    public float seconds;
    public float day;

    public string story;

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

        DontDestroyOnLoad(gameObject);  //else
    }
}
