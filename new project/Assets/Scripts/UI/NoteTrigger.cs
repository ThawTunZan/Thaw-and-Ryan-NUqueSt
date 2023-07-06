using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public string noteType;
    public GameObject notePanel;
    public GameObject noteText;
    public GameObject noteImage;

    public GameObject visualCue;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    private bool playerInRange;

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        HideUI();
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
            CheckType();
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && notePanel.activeSelf 
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            HideUI();
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }

    private void CheckType()
    {
        if (noteType == "Text")
        {
            noteText.SetActive(true);
        }
        else if (noteType == "Image")
        {
            noteImage.SetActive(true);
        }
    }

    private void HideUI()
    {
        noteText.SetActive(false);
        noteImage.SetActive(false);
        notePanel.SetActive(false);
    }
}
