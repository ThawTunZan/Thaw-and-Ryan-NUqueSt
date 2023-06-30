using PlayFab.EconomyModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;

    private GameObject player;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private Health playerHealth;
    private SwordAttack swordAttack;

    private Vector3 mousePosition;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerItems = player.GetComponent<PlayerItems>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("PlayerHitBox").GetComponent<Health>();
        swordAttack = player.GetComponent<SwordAttack>();
        SelectSlot(0);
    }

    private void Update()
    {
        if (!playerItems.disableToolbar)
        {
            CheckAlphaNumericKeys();
            HoldItemFromToolbar(selectedSlot.slotID);
            CheckItemUse();
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

    private void CheckItemUse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClickItemFromToolbar(selectedSlot.slotID);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RightClickItemFromToolbar(selectedSlot.slotID);
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

    private void HoldItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemName == "Stone Hoe")
            {
                HoldHoe(1);
            }
        }
        else
        {
            UnholdHoe();
        }
    }

    private void HoldHoe(int maxReach)
    {
        if (TileHighlighter.instance != null)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TileHighlighter.instance.HighlightTilemap(mousePosition, maxReach);
        }
    }

    private void UnholdHoe()
    {
        if (TileHighlighter.instance != null)
        {
            TileHighlighter.instance.RemoveHighlight(mousePosition);
        }
    }

    private void LeftClickItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemName == "Tomato")
            {
                EatFood(index, 5);
            }
            else if (slot.itemName == "Potato")
            {
                EatFood(index, 5);
            }
            else if (slot.itemName == "Tomato Seeds")
            {
                PlantSeed(index, "Tomato Seeds");
            }
            else if (slot.itemName == "Potato Seeds")
            {
                PlantSeed(index, "Potato Seeds");
            }
            else if (slot.itemName == "Stone Sword")
            {
                SwingTool(2f, 0f, "Sword");
            }
            else if (slot.itemName == "Stone Pickaxe")
            {
                SwingTool(1f, 1f, "Pickaxe");
            }
            else if (slot.itemName == "Stone Hoe")
            {
                SwingTool(0f, 0f, "Hoe");
                UseHoeAddDirt();
            }
            Refresh();
        }
    }

    private void EatFood(int index, int amountToHeal)
    {
        if (playerHealth.health != playerHealth.maxHealth)
        {
            playerHealth.health = Math.Min(playerHealth.health + amountToHeal, playerHealth.maxHealth);
            playerItems.toolbar.Remove(index, 1);
        }
    }

    private void PlantSeed(int index, string itemName)
    {
        playerItems.toolbar.Remove(index, 1);
    }

    private void SwingTool(float swordDamage, float pickaxeDamage, string itemName)
    {
        swordAttack.swordDamage = swordDamage;
        swordAttack.pickaxeDamage = pickaxeDamage;
        playerMovement.AnimateToolAttack(itemName);
    }

    private void UseHoeAddDirt()
    {
        if (TileHighlighter.instance != null)
        {
            TileHighlighter.instance.UseHoeAddDirt(mousePosition);
        }
    }

    private void RightClickItemFromToolbar(int index)
    {
        Inventory.Slot slot = playerItems.toolbar.slots[index];
        if (!slot.IsEmpty)
        {
            if (slot.itemName == "Stone Hoe")
            {
                SwingTool(0f, 0f, "Hoe");
                UseHoeRemoveDirt();
            }
            Refresh();
        }
    }

    private void UseHoeRemoveDirt()
    {
        if (TileHighlighter.instance != null)
        {
            TileHighlighter.instance.UseHoeRemoveDirt(mousePosition);
        }
    }
}
