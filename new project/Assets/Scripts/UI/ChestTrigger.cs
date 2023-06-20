using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            chestPanel.SetActive(true);
            inventoryPanel.SetActive(true);
            playerItems.disableToolbar = true;
            freezePlayerMovement.ToggleMovement();
        }
        else if (playerInRange && playerItems.disableToolbar && chestPanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            dropPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            chestPanel.SetActive(false);
            playerItems.disableToolbar = false;
            freezePlayerMovement.ToggleMovement();
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
