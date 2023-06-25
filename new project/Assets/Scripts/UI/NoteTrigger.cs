using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public GameObject notePanel;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private bool playerInRange;

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            TriggerNote();
        }

    }

    private void TriggerNote()
    {
        if (!playerItems.disableToolbar && !notePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            notePanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && notePanel.activeSelf 
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            notePanel.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
