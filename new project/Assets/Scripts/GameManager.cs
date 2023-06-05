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

    void Awake()
    { 
        if (instance != null)
        {
            Debug.LogError("Found more than one GameManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        instance.inventory = new Inventory(21);
        DontDestroyOnLoad(gameObject);  //else
    }
}
