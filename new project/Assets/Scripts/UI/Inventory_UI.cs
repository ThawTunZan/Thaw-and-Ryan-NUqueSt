using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public string inventoryName;

    public GameObject chestPanel;

    public GameObject slotBlocker;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [Header("Item Description Components")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;
    public Button buyButton;
    public Button sellButton;

    [Header("Shop Components")]
    public GameObject shopPanel;
    public GameObject shopAmountPanel;
    public TextMeshProUGUI headerAmountText;
    public Button buyAmountButton;
    public Button sellAmountButton;
    public TMP_InputField shopAmountText;

    [Header("Drop Panel Components")]
    public GameObject dropPanel;
    public TMP_InputField dropText;

    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    private Canvas canvas;

    private Slot_UI clickedSlot;

    private Slot_UI draggedSlot;
    private Image draggedIcon;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private PlayerMoney playerMoney;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI toolbarInCanvas;
    private Inventory_UI chestInCanvas;
    private Inventory_UI shopInCanvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        inventoryByName.Add("Inventory", playerItems.inventory);
        inventoryByName.Add("Toolbar", playerItems.toolbar);

        playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        toolbarInCanvas = GameObject.Find("Toolbar").GetComponent<Inventory_UI>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        shopInCanvas = GameObject.Find("Shop").GetComponent<Inventory_UI>();

        SetupSlots();
        Refresh();
    }

    void Update()
    {
        if (inventoryPanel != null)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!playerItems.disableToolbar && !chestPanel.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            Refresh();
        }
        else if (playerItems.disableToolbar && !chestPanel.activeSelf && (inventoryPanel.activeSelf || dropPanel.activeSelf)
            && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
        {
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            ItemDescDisable();
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    /*
     * The first for loop is for setting up inventory. The second for loop is for setting up toolbar.
     * If the player's inventory/toolbar has white squares then that means the inventory/toolbar is not properly setup here.
     * This function is called whenever a player enters a new scene, or when the player opens the inventory by pressing TAB.
     * This Refresh needs to happen as there are two different inventories. One inventory is the inventory UI, and the other 
     * inventory is the player's actual inventory (in script). The Refresh will get the items from the player inventory and 
     * make it visible on the inventory UI. Same goes for toolbar. The third if statement is for refreshing chest UI.
     */
    public void Refresh()
    {
        for (int i = 0; i < inventoryInCanvas.slots.Count; i++)
        {
            if (playerItems.inventory.slots[i].itemName != "")
            {
                inventoryInCanvas.slots[i].SetItem(playerItems.inventory.slots[i]);
            }
            else
            {
                inventoryInCanvas.slots[i].SetEmpty();
            }
        }
        for (int i = 0; i < toolbarInCanvas.slots.Count; i++)
        {
            if (playerItems.toolbar.slots[i].itemName != "")
            {
                toolbarInCanvas.slots[i].SetItem(playerItems.toolbar.slots[i]);
            }
            else
            {
                toolbarInCanvas.slots[i].SetEmpty();
            }
        }
        if (chestPanel != null && chestPanel.activeSelf)
        {
            ChestItems chestItems = GameObject.Find(chestInCanvas.inventoryName).GetComponent<ChestItems>();
            chestItems.ChestRefresh();
        }
        if (shopPanel != null && shopPanel.activeSelf)
        {
            ShopItems shopItems = GameObject.Find(shopInCanvas.inventoryName).GetComponent<ShopItems>();
            shopItems.ShopRefresh();
        }
    }

    /*
     * This function is called when a player drags an item out of their inventory/toolbar and drops it out (by releasing when cursor is 
     * outside of any UI). It is attached to RemoveItem_Panel under Canvas.
     */
    public void RemoveAmountUI()
    {
        if (draggedSlot != null)
        {
            Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
            Item itemToDrop = ItemManager.instance.GetItemByName(fromInventory.slots[draggedSlot.slotID].itemName);
            if (itemToDrop != null)
            {
                if (fromInventory.slots[draggedSlot.slotID].maxAllowed == 1)
                {
                    playerItems.DropItem(itemToDrop);
                    fromInventory.Remove(draggedSlot.slotID);
                    Refresh();
                }
                else
                {
                    slotBlocker.SetActive(true);
                    playerItems.disableToolbar = true;
                    dropPanel.SetActive(true);
                    playerMovement.enabled = false;
                }
            }
        }
    }

    /*
     * This function is called when player clicks OK on the drop panel. It handles the item to remove and the amount removed.
     */
    public void Remove()
    {
        Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
        Item itemToDrop = ItemManager.instance.GetItemByName(fromInventory.slots[draggedSlot.slotID].itemName);
        string text = dropText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToDrop);
        if (parseSuccess && amountToDrop <= fromInventory.slots[draggedSlot.slotID].count && amountToDrop >= 0)
        {
            playerItems.DropItem(itemToDrop, amountToDrop);
            fromInventory.Remove(draggedSlot.slotID, amountToDrop);
            Refresh();
        }
        dropPanel.SetActive(false);
        if (!inventoryPanel.activeSelf)
        {
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
        draggedSlot = null;
        slotBlocker.SetActive(false);
    }

    /* 
     * Both these SetTo functions refer to the triple up arrows and triple down arrows on the drop panel
     */ 
    public void SetToMax()
    {
        Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
        dropText.text = fromInventory.slots[draggedSlot.slotID].count.ToString();
    }

    public void SetToMin()
    {
        dropText.text = "0";
    }

    public void ShopSetToMax()
    {
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        shopAmountText.text = fromInventory.slots[clickedSlot.slotID].count.ToString();
    }

    public void ShopSetToMin()
    {
        shopAmountText.text = "0";
    }

    /*
     * The four functions below that start with "Slot" are Event Triggers found in every Slot
     * Each Slot has their own slotID, which is done by SetupSlots() function.
     */
    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.transform.SetParent(canvas.transform);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);
        MoveToMousePosition(draggedIcon.gameObject);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
    }

    public void SlotDrop(Slot_UI slot)
    {
        Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
        Inventory toInventory = inventoryByName[slot.inventoryName];
        MoveSlot(draggedSlot.slotID, fromInventory, slot.slotID, toInventory);
        Refresh();
    }

    public void MoveSlot(int fromIndex, Inventory fromInventory, int toIndex, Inventory toInventory)
    {
        Slot fromSlot = fromInventory.slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];
        int itemCount = fromSlot.count;
        Item fromSlotItem = ItemManager.instance.GetItemByName(fromSlot.itemName);
        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < itemCount; i++)
            {
                toSlot.AddItem(fromSlotItem);
                fromSlot.RemoveItem();
            }
        }
    }

    // Item Description, Also used in Shop
    public void SlotClick(Slot_UI slot)
    {
        clickedSlot = slot;
        if (clickedSlot.itemName != null)
        {
            itemNameText.text = clickedSlot.itemName;
            itemDescText.text = clickedSlot.itemDesc;
            if (shopPanel.activeSelf)
            {
                if (clickedSlot.inventoryName.Substring(0, 4) == "Shop")
                {
                    if (playerMoney.money >= clickedSlot.itemBuyCost)
                    {
                        buyButton.interactable = true;
                    }
                    else
                    {
                        buyButton.interactable = false;
                    }
                    sellButton.interactable = false;
                }
                else
                {
                    buyButton.interactable = false;
                    sellButton.interactable = true;
                }
            }
            itemDescText.text += "\n\nBuy cost: $" + clickedSlot.itemBuyCost;
            itemDescText.text += "\n\nSell cost: $" + clickedSlot.itemSellCost;
        }
    }

    public void ItemDescDisable()
    {
        clickedSlot = null;
        buyButton.interactable = false;
        sellButton.interactable = false;
        itemNameText.text = null;
        itemDescText.text = null;
    }

    public void ClickedBuy()
    {
        Item boughtItem = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        if (boughtItem.data.maxAllowed == 1)
        {
            playerMoney.money -= clickedSlot.itemBuyCost;
            playerItems.inventory.Add(boughtItem);
            ItemDescDisable();
            Refresh();
        }
        else
        {
            headerAmountText.text = "Type amount to buy";
            buyAmountButton.gameObject.SetActive(true);
            sellAmountButton.gameObject.SetActive(false);
            slotBlocker.SetActive(true);
            shopAmountPanel.SetActive(true);
        }
    }

    public void BuyFromShop()
    {
        Inventory fromShop = inventoryByName[clickedSlot.inventoryName];
        Item itemToBuy = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        string text = shopAmountText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToBuy);
        if (parseSuccess && playerMoney.money >= amountToBuy * itemToBuy.data.itemBuyCost && amountToBuy >= 0
            && amountToBuy <= fromShop.slots[clickedSlot.slotID].count)
        {
            playerItems.inventory.Add(itemToBuy, amountToBuy);
            fromShop.Remove(clickedSlot.slotID, amountToBuy);
            playerMoney.money -= amountToBuy * itemToBuy.data.itemBuyCost;
            Refresh();
        }
        shopAmountPanel.SetActive(false);
        slotBlocker.SetActive(false);
        ItemDescDisable();
    }

    public void ClickedSell()
    {
        Item soldItem = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        if (soldItem.data.maxAllowed == 1)
        {
            playerMoney.money += clickedSlot.itemBuyCost;
            playerItems.inventory.Remove(clickedSlot.slotID);
            ItemDescDisable();
            Refresh();
        }
        else
        {
            headerAmountText.text = "Type amount to sell";
            buyAmountButton.gameObject.SetActive(false);
            sellAmountButton.gameObject.SetActive(true);
            slotBlocker.SetActive(true);
            shopAmountPanel.SetActive(true);
        }
    }

    public void SellFromInventory()
    {
        Item itemToSell = ItemManager.instance.GetItemByName(clickedSlot.itemName);
        Inventory fromInventory = inventoryByName[clickedSlot.inventoryName];
        string text = shopAmountText.text;
        bool parseSuccess = int.TryParse(text.Trim(), out int amountToSell);
        if (parseSuccess && playerItems.inventory.slots[clickedSlot.slotID].count >= amountToSell && amountToSell >= 0)
        {
            fromInventory.Remove(clickedSlot.slotID, amountToSell);
            playerMoney.money += amountToSell * itemToSell.data.itemSellCost;
            Refresh();
        }
        shopAmountPanel.SetActive(false);
        slotBlocker.SetActive(false);
        ItemDescDisable();
    }

    /*
     * This function makes the image of the item being dragged to follow the cursor
     */
    private void MoveToMousePosition(GameObject toMove)
    {
        if(canvas != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    /*
     * This handles the slotID of the inventory and toolbar slots. slotID is used in dragging items. If there is an error in dragging
     * items, then something might be wrong in assigning the inventoryName of each slots. It may be null.
     */
    void SetupSlots()
    {
        int counter = 0;
        foreach (Slot_UI slot in slots)
        {
            slot.inventoryName = inventoryName;
            slot.slotID = counter;
            counter++;
        }
    }
}
