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
    public int gameScene;
    public float maxHealth;
    public Inventory inventory;
    public Inventory toolbar;
    public string story;
    public string storyState;
    public List<int> weaponsmithNPC;           // quest progress for WeaponSmith NPC

    
    //default value;
    public GameData()
    {
        playerPosition = Vector3.zero;
        gameScene = 3;
        maxHealth = 100;
        inventory = new Inventory(21);
        toolbar = new Inventory(7);
        storyState = "";
        weaponsmithNPC = new List<int>() { 0, 0, 0};
    }
}
