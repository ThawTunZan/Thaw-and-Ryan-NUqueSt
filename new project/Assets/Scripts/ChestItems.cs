using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItems : MonoBehaviour, IDataPersistence
{
    public string chestName;
    public Inventory chestInventory;
    public Dictionary<string, Inventory> stringToChestManager;
    public Dictionary<string, Inventory> stringToChestData;

    public PlayerPositionSO startingPosition;
    private SpriteRenderer playerRenderer;

    private Inventory_UI chestInCanvas;

    public bool hasAddedToChest;

    private void Start()
    {
        gameObject.name = chestName;
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene)
        {
            hasAddedToChest = GameManager.instance.hasAddedToChest;
            // chest0: PlayerHouse
            // chest1: UNUSED
            // chest2: Cave_1
            if (!hasAddedToChest)
            {
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Stone Hoe"));
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Tomato Seed"), 10);
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Potato Seed"), 10);
                //GameManager.instance.chest1.Add(ItemManager.instance.GetItemByName("Diamond Ore"), 2);
                GameManager.instance.chest2.Add(ItemManager.instance.GetItemByName("Stone Pickaxe"));
                hasAddedToChest = true;
            }
            chestInventory = new Inventory(chestName, 21);
            if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
            {
                chestInventory = GameManager.instance.chestList[lastDigit];
            }
        }
    }

    private void Update()
    {
        GameManager.instance.hasAddedToChest = hasAddedToChest;
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            GameManager.instance.chestList[lastDigit] = chestInventory;
        }
    }

    public void ChestRefresh()
    {
        for (int i = 0; i < chestInCanvas.slots.Count; i++)
        {
            chestInCanvas.slots[i].inventoryName = chestName;
            if (chestInventory.slots[i].itemName != "")
            {
                chestInCanvas.slots[i].SetItem(chestInventory.slots[i]);
            }
            else
            {
                chestInCanvas.slots[i].SetEmpty();
            }
        }
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        if (playerRenderer.flipX)
        {
            Instantiate(item, spawnLocation + new Vector2(-0.2f, -0.1f), Quaternion.identity);
        }
        else
        {
            Instantiate(item, spawnLocation + new Vector2(0.2f, -0.1f), Quaternion.identity);
        }
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public void LoadData(GameData data)
    {
        hasAddedToChest = data.hasAddedToChest;
        chestInventory = new Inventory(chestName, 21);
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            chestInventory = data.chestList[lastDigit];
        }
        foreach (Inventory.Slot slot in chestInventory.slots)
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
    }

    public void SaveData(GameData data)
    {
        data.hasAddedToChest = hasAddedToChest;
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            data.chestList[lastDigit] = chestInventory;
        }
    }
}
