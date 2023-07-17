using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestboardTrigger : MonoBehaviour
{
    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public GameObject completedQuestPanel;
    private bool playerInRange;
    public GameObject visualCue;

    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            QuestboardUI();
        }
    }

    private void QuestboardUI()
    {
        if (!playerItems.disableToolbar && !completedQuestPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
            completedQuestPanel.SetActive(true);
        }
        else if (playerItems.disableToolbar && completedQuestPanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            completedQuestPanel.SetActive(false);
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
}
