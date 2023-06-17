using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public GameObject chestPanel;
    public GameObject inventoryPanel;
    public GameObject dropPanel;

    private PlayerItems playerItems;
    private FreezePlayerMovement freezePlayerMovement;

    private bool playerInRange;

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();

    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleChest();
        }
    }

    private void ToggleChest()
    {
        if (chestPanel != null)
        {
            if (!chestPanel.activeSelf)
            {
                chestPanel.SetActive(true);
                playerItems.disableToolbar = true;
                freezePlayerMovement.ToggleMovement();
                //Refresh();
            }
            else
            {
                dropPanel.SetActive(false);
                chestPanel.SetActive(false);
                playerItems.disableToolbar = false;
                freezePlayerMovement.ToggleMovement();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
