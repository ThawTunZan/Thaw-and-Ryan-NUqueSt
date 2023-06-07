using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;
    public static GameManager instance;
    public int level;
    public float exp;
    public Inventory inventory;
    public Inventory toolbar;

    // for day and night system
    public float hours;
    public float minutes;
    public float seconds;
    public float day;

    void Awake()
    { 
        if (instance != null)
        {
            //Debug.LogError("Found more than one GameManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        instance.inventory = new Inventory(21);
        instance.toolbar = new Inventory(7);
        DontDestroyOnLoad(gameObject);  //else
    }
}
