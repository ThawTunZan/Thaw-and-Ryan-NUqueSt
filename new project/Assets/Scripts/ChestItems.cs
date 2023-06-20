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

    private void Start()
    {
        gameObject.name = chestName;
        //stringToChestManager.Add("Chest0", GameManager.instance.chest0);
        //stringToChestManager.Add("Chest1", GameManager.instance.chest1);
        playerRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        if (startingPosition.transittedScene)
        {
            //chestInventory = new Inventory(chestName, 21);
            //chestInventory = stringToChestManager[chestName];
            //chestInventory = GameManager.instance.chest0;
            //startingPosition.transittedScene = false;
            chestInventory = new Inventory(chestName, 21);
            if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
            {
                chestInventory = GameManager.instance.chestList[lastDigit];
            }
            //if (chestName == "Chest0") 
            //{
            //    chestInventory = GameManager.instance.chest0;
            //}
            //else if (chestName == "Chest1")
            //{
            //    chestInventory = GameManager.instance.chest1;
            //}
        }
    }

    private void Update()
    {
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            GameManager.instance.chestList[lastDigit] = chestInventory;
        }
        //if (chestName == "Chest0")
        //{
        //    GameManager.instance.chest0 = chestInventory;
        //}
        //else if (chestName == "Chest1")
        //{
        //    GameManager.instance.chest1 = chestInventory;
        //}
        //stringToChestManager[chestName] = chestInventory;
        //GameManager.instance.chest0 = chestInventory;
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
        //stringToChestData.Add("Chest0", data.chest0);
        //stringToChestData.Add("Chest1", data.chest1);
        chestInventory = new Inventory(chestName, 21);
        //chestInventory = stringToChestData[chestName];
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
        //stringToChestData.Add("Chest0", data.chest0);
        //stringToChestData.Add("Chest1", data.chest1);
        //stringToChestData[chestName] = chestInventory;
        if (int.TryParse(chestName.Substring(chestName.Length - 1), out int lastDigit))
        {
            data.chestList[lastDigit] = chestInventory;
        }
    }
}
