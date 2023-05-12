using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public PlayerItems player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    public PlayerMovement movement;

    float original_speed;

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
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
            movement.movespeed = original_speed;
        }
    }

    void Setup()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].type != CollectableType.NONE)
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
}
