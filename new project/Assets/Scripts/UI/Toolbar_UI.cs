using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerItems = player.GetComponent<PlayerItems>();
        playerMovement = player.GetComponent<PlayerMovement>();
        SelectSlot(0);
    }

    private void Update()
    {
        CheckAlphaNumericKeys();
        CheckLeftClick();
    }

    public void SelectSlot(int index)
    {
        if(toolbarSlots.Count == 7)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighlight(true);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        for (int i = 0; i < toolbarSlots.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
            }
        }
    }

    private void CheckLeftClick()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            UseItemFromToolbar(selectedSlot.slotID);
        }
    }

    private void UseItemFromToolbar(int index)
    {
        Slot_UI selectedSlot = toolbarSlots[index];
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            Debug.Log(slot.itemName);
            //if (slot.itemName == "Tomato")
            //{
            //    // Perform the eat action here
            //    EatTomato();

            //    // Decrease the quantity of the item in the slot
            //    playerItems.toolbar.Remove(index, 1);
            //    selectedSlot.SetItem(slot);
            //}
            playerMovement.AnimateSwordAttack();
        }
    }

    private void EatTomato()
    {
        // Add the logic for eating a tomato here
    }
}
