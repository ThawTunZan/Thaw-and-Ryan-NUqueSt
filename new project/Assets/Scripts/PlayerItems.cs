using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public bool disableToolbar;
    public Inventory inventory;
    public Inventory toolbar;

    public PlayerPositionSO startingPosition;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            inventory = new Inventory("Inventory", 21);
            inventory = GameManager.instance.inventory;
            toolbar = new Inventory("Toolbar", 7);
            toolbar = GameManager.instance.toolbar;
        }
        //Uncomment below lines if don't want to load from sample scene
        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);
    }

    private void Update()
    {
        GameManager.instance.inventory = inventory;
        GameManager.instance.toolbar = toolbar;
    }

    public void LoadData(GameData data)
    {
        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);
        inventory = data.inventory;
        toolbar = data.toolbar;
        foreach (Slot slot in inventory.slots)
        {
            LoadItemSprite(slot);
        }
        foreach (Slot slot in toolbar.slots)
        {
            LoadItemSprite(slot);
        }
    }

    public void LoadItemSprite(Slot slot)
    {
        if (slot.itemName == "Tomato")
        {
            slot.icon = Resources.Load<Sprite>("Farming/Tomato");
        }
        else if (slot.itemName == "Potato")
        {
            slot.icon = Resources.Load<Sprite>("Farming/Potato");
        }
        else if (slot.itemName == "Potato Seed")
        {
            slot.icon = Resources.Load<Sprite>("Farming/Potato_Seed");
        }
        else if (slot.itemName == "Tomato Seed")
        {
            slot.icon = Resources.Load<Sprite>("Farming/Tomato_Seed");
        }
        else if (slot.itemName == "Stone Sword")
        {
            slot.icon = Resources.Load<Sprite>("Weapons/Stone_Sword");
        }
        else if (slot.itemName == "Stone Pickaxe")
        {
            slot.icon = Resources.Load<Sprite>("Weapons/Stone_Pickaxe");
        }
        else if (slot.itemName == "Stone Hoe")
        {
            slot.icon = Resources.Load<Sprite>("Weapons/Stone_Hoe");
        }
        else if (slot.itemName == "Stone Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Stone_Ore");
        }
        else if (slot.itemName == "Coal Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Coal_Ore");
        }
        else if (slot.itemName == "Copper Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Copper_Ore");
        }
        else if (slot.itemName == "Iron Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Iron_Ore");
        }
        else if (slot.itemName == "Gold Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Gold_Ore");
        }
        else if (slot.itemName == "Emerald Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Emerald_Ore");
        }
        else if (slot.itemName == "Diamond Ore")
        {
            slot.icon = Resources.Load<Sprite>("Ores/Diamond_Ore");
        }
    }

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
        data.toolbar = toolbar;
    }
}
