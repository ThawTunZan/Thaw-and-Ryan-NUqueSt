using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour, IDataPersistence
{
    public string shopName;
    public Inventory shopInventory;

    public PlayerPositionSO startingPosition;

    private Inventory_UI shopInCanvas;

    private float dayChecker;

    private void Start()
    {
        gameObject.name = shopName;
        shopInCanvas = GameObject.Find("Shop").GetComponent<Inventory_UI>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            dayChecker = GameManager.instance.dayChecker;
            // shop0: blacksmith
            // shop1: generalshop
            shopInventory = new Inventory(shopName, 21);
            if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
            {
                shopInventory = GameManager.instance.shopList[lastDigit];
            }
        }
    }

    private void Update()
    {
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            GameManager.instance.shopList[lastDigit] = shopInventory;
        }
        if (GameManager.instance.day > dayChecker)
        {
            EmptyShop(GameManager.instance.shop0);
            EmptyShop(GameManager.instance.shop1);
            ShopRestock();
            dayChecker = GameManager.instance.day;
        }
    }
    
    private void EmptyShop(Inventory shop)
    {
        for (int i = 0; i < 21; i++)
        {
            if (shop.slots[i] == null)
            {
                break;
            }
            else
            {
                shop.Remove(i, shop.slots[i].count);
            }
        }
    }

    private void ShopRestock()
    {
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Stone Hoe"));
        GameManager.instance.shop0.Add(ItemManager.instance.GetItemByName("Stone Pickaxe"));
        GameManager.instance.shop1.Add(ItemManager.instance.GetItemByName("Tomato Seed"), 10);
        GameManager.instance.shop1.Add(ItemManager.instance.GetItemByName("Potato Seed"), 10);
    }

    public void ShopRefresh()
    {
        for (int i = 0; i < shopInCanvas.slots.Count; i++)
        {
            shopInCanvas.slots[i].inventoryName = shopName;
            if (shopInventory.slots[i].itemName != "")
            {
                shopInCanvas.slots[i].SetItem(shopInventory.slots[i]);
            }
            else
            {
                shopInCanvas.slots[i].SetEmpty();
            }
        }
    }

    public void LoadData(GameData data)
    {
        shopInventory = new Inventory(shopName, 21);
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            shopInventory = data.shopList[lastDigit];
        }
        foreach (Inventory.Slot slot in shopInventory.slots)
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
        if (int.TryParse(shopName.Substring(shopName.Length - 1), out int lastDigit))
        {
            data.shopList[lastDigit] = shopInventory;
        }
    }
}
