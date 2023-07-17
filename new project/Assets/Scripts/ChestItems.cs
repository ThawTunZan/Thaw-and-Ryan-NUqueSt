using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItems : MonoBehaviour, IDataPersistence
{
    public string chestName;
    public Inventory chestInventory;

    public PlayerPositionSO startingPosition;

    private Inventory_UI chestInCanvas;

    private bool hasAddedToChest;

    private void Start()
    {
        gameObject.name = chestName;
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            hasAddedToChest = GameManager.instance.hasAddedToChest;
            // chest0: PlayerHouse
            // chest1: UNUSED
            // chest2: Cave_1
            if (!hasAddedToChest)
            {
                GameManager.instance.chest0 = new Inventory("Chest0", 21);
                GameManager.instance.chest1 = new Inventory("Chest1", 21);
                GameManager.instance.chest2 = new Inventory("Chest2", 21);
                GameManager.instance.chestList = new List<Inventory> 
                { 
                    GameManager.instance.chest0,
                    GameManager.instance.chest1,
                    GameManager.instance.chest2
                };
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Stone Hoe"));
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Tomato Seed"), 10);
                GameManager.instance.chest0.Add(ItemManager.instance.GetItemByName("Potato Seed"), 10);
                GameManager.instance.chest1.Add(ItemManager.instance.GetItemByName("Copper Sword"));
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
            string itemName = slot.itemName;
            string directory = "";
            if (slot.itemType == "Seed" || slot.itemType == "Food")
            {
                directory = "Farming/";
            }
            else if (slot.itemType == "Ore")
            {
                directory = "Ores/";
            }
            else if (slot.itemType == "Sword" || slot.itemType == "Pickaxe" || slot.itemType == "Hoe")
            {
                directory = "Weapons/";
            }
            slot.icon = Resources.Load<Sprite>(directory + itemName.Replace(" ", "_"));
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
