using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItems : MonoBehaviour, IDataPersistence
{
    public Inventory chestInventory;
    public PlayerPositionSO startingPosition;
    public SpriteRenderer playerRenderer;

    private void Start()
    {
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (startingPosition.transittedScene)
        {
            chestInventory = new Inventory("ChestInventory", 21);
            chestInventory = GameManager.instance.chestInventory;
            //startingPosition.transittedScene = false;
        }
    }

    private void Update()
    {
        GameManager.instance.chestInventory = chestInventory;
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
        chestInventory = new Inventory("ChestInventory", 21);
        chestInventory = data.chestInventory;
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
        data.chestInventory = chestInventory;
    }
}
