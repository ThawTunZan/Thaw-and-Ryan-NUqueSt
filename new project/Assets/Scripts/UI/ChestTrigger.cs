using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public GameObject chestPanel;
    public GameObject inventoryPanel;
    public GameObject dropPanel;

    private Inventory_UI inventoryInCanvas;
    private Inventory_UI chestInCanvas;
    private ChestItems chestItems;
    private PlayerItems playerItems;
    private FreezePlayerMovement freezePlayerMovement;
    private GameObject inventoryNewPosition;
    private GameObject inventoryOriginPosition;

    private bool playerInRange;

    private void Start()
    {
        inventoryInCanvas = GameObject.Find("Inventory").GetComponent<Inventory_UI>();
        chestInCanvas = GameObject.Find("ChestInv").GetComponent<Inventory_UI>();
        chestItems = gameObject.transform.parent.gameObject.GetComponent<ChestItems>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();
        inventoryNewPosition = GameObject.Find("InventoryNewPosition");
        inventoryOriginPosition = GameObject.Find("InventoryOriginPosition");
    }

    private void Update()
    {
        if (playerInRange && !playerItems.disableToolbar && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.transform.position = inventoryNewPosition.transform.position;
            inventoryInCanvas.inventoryByName.Add(chestItems.chestName, chestItems.chestInventory);
            chestInCanvas.inventoryName = chestItems.chestName;
            chestPanel.SetActive(true);
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            inventoryInCanvas.Refresh();
            ChestRefresh();
            freezePlayerMovement.ToggleMovement();
        }
        else if (playerInRange && playerItems.disableToolbar && chestPanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            inventoryPanel.transform.position = inventoryOriginPosition.transform.position;
            chestInCanvas.inventoryName = null;
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            chestPanel.SetActive(false);
            playerItems.disableToolbar = false;
            freezePlayerMovement.ToggleMovement();
        }
    }

    private void ChestRefresh()
    {
        for (int i = 0; i < chestInCanvas.slots.Count; i++)
        {
            if (chestItems.chestInventory.slots[i].itemName != "")
            {
                chestInCanvas.slots[i].SetItem(playerItems.inventory.slots[i]);
            }
            else
            {
                chestInCanvas.slots[i].SetEmpty();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
