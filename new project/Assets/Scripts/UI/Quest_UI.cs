using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using TMPro;
using UnityEngine.SceneManagement;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    private TextMeshProUGUI activeQuests;

    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();
    
    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        activeQuests = GameObject.Find("ActiveQuests").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        Setup();
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Q))
        {
            activeQuests.gameObject.SetActive(false);
            questPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && questPanel.activeSelf && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)))
        {
            activeQuests.gameObject.SetActive(true);
            questPanel.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    void Setup()
    {
        if (SceneManager.GetActiveScene().name != "IntroTutorial")
        {
            activeQuests.text = "Active Quests:\n";
        }
        string tempQuests = "";
        if (questSlots.Count == playerQuests.questList.questSlots.Count)
        {
            for (int i = 0; i < questSlots.Count; i++)
            {
                if (playerQuests.questList.questSlots[i].count == 1)
                {
                    questSlots[i].SetItem(playerQuests.questList.questSlots[i]);
                    tempQuests += playerQuests.questList.questSlots[i].questName + " - ";
                    if (!playerQuests.questList.questSlots[i].done)
                    {
                        tempQuests += "Not ";
                    }
                    tempQuests += "Done\n";
                }
                else
                {
                    questSlots[i].SetEmpty();
                }
            }
        }
        activeQuests.text += tempQuests;
    }
}
