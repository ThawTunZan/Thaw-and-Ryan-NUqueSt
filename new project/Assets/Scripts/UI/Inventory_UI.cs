using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public PlayerItems player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    [Header("Drop Panel Components")]
    public GameObject dropPanel;
    public Button dropButton;
    public TMP_InputField dropText;

    private Slot_UI draggedSlot;
    private Image draggedIcon;

    private PlayerMovement movement;
    float original_speed;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        SetupSlots();
        Refresh();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                original_speed = movement.movespeed;
                movement.movespeed = 0;
                Refresh();
            }
            else
            {
                dropPanel.SetActive(false);
                inventoryPanel.SetActive(false);
                movement.movespeed = original_speed;
            }
        }
    }

    /*
     * The first if is for setting up inventory. The second if is for setting up toolbar.
     * If the player's inventory and/or toolbar has white squares then that means the inventory and/or toolbar is not properly setup here.
     */
    void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }

        else if (slots.Count == player.toolbar.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.toolbar.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.toolbar.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void RemoveAmountUI()
    {
        dropPanel.SetActive(true);
    }

    public void Remove()
    {
        Item itemToDrop = ItemManager.instance.GetItemByName(
            player.inventory.slots[draggedSlot.slotID].itemName);

        if (itemToDrop != null)
        {
            string text = dropText.text;
            bool parseSuccess = int.TryParse(text.Trim(), out int amountToDrop);
            if (parseSuccess && amountToDrop <= player.inventory.slots[draggedSlot.slotID].count && amountToDrop >= 0)
            {
                player.DropItem(itemToDrop, amountToDrop);
                player.inventory.Remove(draggedSlot.slotID, amountToDrop);
                Refresh();
            }
            else
            {
                Debug.Log("FAILED TO DROP!!!");
            }
        }

        dropPanel.SetActive(false);
        draggedSlot = null;
    }

    public void SetToMax()
    {
        dropText.text = player.inventory.slots[draggedSlot.slotID].count.ToString();
    }

    public void SetToMin()
    {
        dropText.text = "1";
    }

    /*
     * The four functions below that start with "Slot" are Event Triggers found in every Slot
     * Each Slot has their own slotID
     */
    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.transform.SetParent(canvas.transform);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);

        MoveToMousePosition(draggedIcon.gameObject);

        Debug.Log("Starting Drag " + draggedSlot.slotID);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("In Drag " + draggedSlot.slotID);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
        Debug.Log("Ending Drag " + draggedSlot.slotID);
    }

    public void SlotDrop(Slot_UI slot)
    {
        player.inventory.MoveSlot(draggedSlot.slotID, slot.slotID);
        Refresh();
        Debug.Log("Dropping " + draggedSlot.slotID + " on " + slot.slotID);
    }

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

    void SetupSlots()
    {
        int counter = 0;
        foreach (Slot_UI slot in slots)
        {
            slot.slotID = counter;
            counter++;
        }
    }
}
