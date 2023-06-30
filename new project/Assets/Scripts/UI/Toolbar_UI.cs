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
            if (slot.itemName == "Tomato" || slot.itemName == "Potato")
            {
                EatFood(index);
            }
            else if (slot.itemName == "Tomato Seeds" || slot.itemName == "Potato Seeds")
            {
                PlantSeed(index);
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

    private void EatFood(int index)
    {
        playerItems.toolbar.Remove(index, 1);
    }

    private void PlantSeed(int index)
    {
        playerItems.toolbar.Remove(index, 1);
    }

    private void SwingSword()
    {
        swordAttack.swordDamage = 1f;
        swordAttack.pickaxeDamage = 0f;
        playerMovement.AnimateToolAttack("Sword");
    }

    private void SwingPickaxe()
    {
        swordAttack.swordDamage = 1f;
        swordAttack.pickaxeDamage = 1f;
        playerMovement.AnimateToolAttack("Pickaxe");
    }

    private void SwingHoe()
    {
        swordAttack.swordDamage = 1f;
        swordAttack.pickaxeDamage = 0f;
        playerMovement.AnimateToolAttack("Hoe");
    }
}
