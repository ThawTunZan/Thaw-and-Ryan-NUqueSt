using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForLoopPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public GameObject puzzleText;
    public TMP_InputField puzzleInput;

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
        if (!playerItems.disableToolbar && !puzzlePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            puzzlePanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && puzzlePanel.activeSelf
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

    private void HideUI()
    {
        puzzlePanel.SetActive(false);
        puzzleText.SetActive(false);
        puzzleInput.gameObject.SetActive(false);
    }
}
