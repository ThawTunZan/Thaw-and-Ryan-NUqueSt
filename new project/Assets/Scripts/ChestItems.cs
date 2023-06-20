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
    public SpriteRenderer playerRenderer;

    private Inventory_UI chestInCanvas;

    private void Start()
    {
        gameObject.name = chestName;
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene)
        {
            chestInventory = new Inventory(chestName, 21);
            if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
            {
                chestInventory = GameManager.instance.chestList[lastDigit];
            }
        }
    }

    private void Update()
    {
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
        chestInventory = new Inventory(chestName, 21);
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            chestInventory = data.chestList[lastDigit];
        }
        foreach (Inventory.Slot slot in chestInventory.slots)
        {
            //print(slot.itemName);
            if (slot.itemName == "Tomato")
            {
                slot.icon = Resources.Load<Sprite>("Farming/Tomato");
            }
            else if (slot.itemName == "Potato")
            {
                slot.icon = Resources.Load<Sprite>("Farming/Potato");
            }
            else if (slot.itemName == "Potato Seeds")
            {
                print("POTATOATOATOO Seeeeeeeeeeeed");
                slot.icon = Resources.Load<Sprite>("Farming/Potato_Seed");
            }
            else if (slot.itemName == "Tomato Seeds")
            {
                slot.icon = Resources.Load<Sprite>("Farming/Tomato_Seed");
            }
            else if (slot.itemName == "Rusty Sword")
            {
                slot.icon = Resources.Load<Sprite>("Weapon/Rusty_Sword");
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            data.chestList[lastDigit] = chestInventory;
        }
    }
}
