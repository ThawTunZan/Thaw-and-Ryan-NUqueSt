using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private SwordAttack swordAttack;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerItems = player.GetComponent<PlayerItems>();
        playerMovement = player.GetComponent<PlayerMovement>();
        swordAttack = player.GetComponent<SwordAttack>();
        SelectSlot(0);
    }

    private void Update()
    {
        if (!playerItems.disableToolbar)
        {
            CheckAlphaNumericKeys();
            CheckLeftClick();
        }
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseItemFromToolbar(selectedSlot.slotID);
        }
    }

    void Refresh()
    {
        for (int i = 0; i < gameObject.GetComponent<Inventory_UI>().slots.Count; i++)
        {
            if (playerItems.toolbar.slots[i].itemName != "")
            {
                gameObject.GetComponent<Inventory_UI>().slots[i].SetItem(playerItems.toolbar.slots[i]);
            }
            else
            {
                gameObject.GetComponent<Inventory_UI>().slots[i].SetEmpty();
            }
        }
    }

    private void UseItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemName == "Tomato")
            {
                EatTomato();
                playerItems.toolbar.Remove(index, 1);
            }
            else if (slot.itemName == "Tomato Seeds")
            {
                PlantTomato();
            }
            else if (slot.itemName == "Stone Sword")
            {
                SwingSword();
            }
            else if (slot.itemName == "Stone Pickaxe")
            {
                SwingPickaxe();
            }
            else if (slot.itemName == "Stone Hoe")
            {
                SwingHoe();
            }
            Refresh();
        }
    }

    private void EatTomato()
    {
        // Add the logic for eating a tomato here
    }

    private void PlantTomato()
    {
        // Add the logic for planting a tomato here
    }

    private void SwingSword()
    {
        swordAttack.swordSideAttackObject.tag = "SwordAttack";
        swordAttack.swordUpDownAttackObject.tag = "SwordAttack";
        swordAttack.swordDamage = 1f;
        playerMovement.AnimateToolAttack("Sword");
    }

    private void SwingPickaxe()
    {
        swordAttack.swordSideAttackObject.tag = "PickaxeAttack";
        swordAttack.swordUpDownAttackObject.tag = "PickaxeAttack";
        swordAttack.pickaxeDamage = 1f;
        playerMovement.AnimateToolAttack("Pickaxe");
    }

    private void SwingHoe()
    {
        playerMovement.AnimateToolAttack("Hoe");
    }
}
