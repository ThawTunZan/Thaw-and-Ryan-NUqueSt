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
    [SerializeField] private Canvas canvas;

    public GameObject inventoryPanel;

    public PlayerItems player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    public GameObject dropPanel;

    public Button dropButton;

    public TextMeshProUGUI dropText;

    private Slot_UI draggedSlot;

    private Image draggedIcon;

    public PlayerMovement movement;

    float original_speed;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
            if (parseSuccess)
            {
                player.DropItem(itemToDrop, amountToDrop);
                player.inventory.Remove(draggedSlot.slotID, amountToDrop);
                Refresh();
            }
            else
            {
                Debug.Log("FAILED TO DROP!!!");
            }


            //int amountToDrop = int.Parse(dropText.text);

            //player.DropItem(itemToDrop, player.inventory.slots[draggedSlot.slotID].count);
            //player.inventory.Remove(draggedSlot.slotID, player.inventory.slots[draggedSlot.slotID].count);
        }

        dropPanel.SetActive(false);
        draggedSlot = null;
    }

    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.transform.SetParent(canvas.transform);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);

        MoveToMousePosition(draggedIcon.gameObject);
        //Debug.Log("Start Drag: " + draggedSlot.name);
    }

    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);
        //Debug.Log("Dragging: " + draggedSlot.name);
    }

    public void SlotEndDrag()
    {
        Destroy(draggedIcon.gameObject);
        draggedIcon = null;
        //Debug.Log("Done Dragging: " + draggedSlot.name);
    }

    public void SlotDrop(Slot_UI slot)
    {
        //Debug.Log("Dropped " + draggedSlot.name + " on " + slot.name);
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
}
