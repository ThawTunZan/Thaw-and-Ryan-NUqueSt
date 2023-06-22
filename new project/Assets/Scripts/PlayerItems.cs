using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour, IDataPersistence
{
    public bool disableToolbar;
    public Inventory inventory;
    public Inventory toolbar;
    public PlayerPositionSO startingPosition;
    public SpriteRenderer playerRenderer;

    private void Start()
    {
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            inventory = new Inventory("Inventory", 21);
            inventory = GameManager.instance.inventory;
            toolbar = new Inventory("Toolbar", 7);
            toolbar = GameManager.instance.toolbar;
        }
    }

    private void Update()
    {
        GameManager.instance.inventory = inventory;
        GameManager.instance.toolbar = toolbar;
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
        inventory = new Inventory("Inventory", 21);
        toolbar = new Inventory("Toolbar", 7);
        inventory = data.inventory;
        toolbar = data.toolbar;
        foreach (Inventory.Slot slot in inventory.slots)
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
               // print("POTATOATOATOO Seeeeeeeeeeeed");
                slot.icon = Resources.Load<Sprite>("Farming/Potato_Seed");
            }
            else if (slot.itemName == "Tomato Seeds")
            {
                slot.icon = Resources.Load<Sprite>("Farming/Tomato_Seed");
            }
            else if (slot.itemName == "Rusty Sword")
            {
                slot.icon = Resources.Load<Sprite>("Weapons/Rusty_Sword");
            }
        }
        foreach (Inventory.Slot slot in toolbar.slots)
        {
           // print(slot.itemName);
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
                slot.icon = Resources.Load<Sprite>("Farming/Potato_Seed");
            }
            else if (slot.itemName == "Tomato Seeds")
            {
                slot.icon = Resources.Load<Sprite>("Farming/Tomato_Seed");
            }
            else if (slot.itemName == "Rusty Sword")
            {
                slot.icon = Resources.Load<Sprite>("Weapons/Rusty_Sword");
            }
        }
    }

    public void SaveData(GameData data)
    {
        data.inventory = inventory;
        data.toolbar = toolbar;
    }
}
