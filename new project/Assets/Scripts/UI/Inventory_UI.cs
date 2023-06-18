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

    public List<Slot_UI> slots = new List<Slot_UI>();

    [Header("Drop Panel Components")]
    public GameObject dropPanel;
    public Button dropButton;
    public TMP_InputField dropText;

    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    private Canvas canvas;

    private Slot_UI draggedSlot;
    private Image draggedIcon;

    private PlayerItems playerItems;
    private FreezePlayerMovement freezePlayerMovement;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI toolbarInCanvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        inventoryByName.Add("Inventory", playerItems.inventory);
        inventoryByName.Add("Toolbar", playerItems.toolbar);

        freezePlayerMovement = canvas.GetComponent<FreezePlayerMovement>();

        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        toolbarInCanvas = GameObject.Find("Toolbar").GetComponent<Inventory_UI>();

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
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            freezePlayerMovement.ToggleMovement();
            Refresh();
        }
        else if (playerItems.disableToolbar && (inventoryPanel.activeSelf || dropPanel.activeSelf)
            && (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)))
        {
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            playerItems.disableToolbar = false;
            freezePlayerMovement.ToggleMovement();
        }
    }

    /*
     * The first for loop is for setting up inventory. The second for loop is for setting up toolbar.
     * If the player's inventory/toolbar has white squares then that means the inventory/toolbar is not properly setup here.
     * This function is called whenever a player enters a new scene, or when the player opens the inventory by pressing TAB.
     * This Refresh needs to happen as there are two different inventories. One inventory is the inventory UI, and the other 
     * inventory is the player's actual inventory (in script). The Refresh will get the items from the player inventory and 
     * make it visible on the inventory UI. Same goes for toolbar.
     */
    void Refresh()
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
    }

    /*
     * This function is called when a player drags an item out of their inventory/toolbar and drops it out (by releasing when cursor is 
     * outside of any UI). It is attached to RemoveItem_Panel under Canvas.
     */
    public void RemoveAmountUI()
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
                playerItems.disableToolbar = true;
                dropPanel.SetActive(true);
                freezePlayerMovement.ToggleMovement();
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
        if (!inventoryPanel.activeSelf)
        {
            playerItems.disableToolbar = false;
        }
        dropPanel.SetActive(false);
        freezePlayerMovement.ToggleMovement();
        draggedSlot = null;
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
        dropText.text = "1";
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
        //Debug.Log("Starting Drag " + draggedSlot.slotID);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        //Debug.Log("In Drag " + draggedSlot.slotID);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
        //Debug.Log("Ending Drag " + draggedSlot.slotID);
    }

    public void SlotDrop(Slot_UI slot)
    {
        Inventory fromInventory = inventoryByName[draggedSlot.inventoryName];
        Inventory toInventory = inventoryByName[slot.inventoryName];
        MoveSlot(draggedSlot.slotID, fromInventory, slot.slotID, toInventory);
        Refresh();
        //Debug.Log("Dropping " + draggedSlot.slotID + " on " + slot.slotID);
    }

    public void MoveSlot(int fromIndex, Inventory fromInventory, int toIndex, Inventory toInventory)
    {
        Slot fromSlot = fromInventory.slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];
        int itemCount = fromSlot.count;

        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < itemCount; i++)
            {
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed);
                fromSlot.RemoveItem();
            }
        }
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
     * This handles the slotID of the inventory and toolbar slots. slotID is used in dragging items.
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
