using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using TMPro;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    private TextMeshProUGUI activeQuests;

    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private PlayerTutorial playerTutorial;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();

    private Dictionary<string, int> tempDict = new Dictionary<string, int>();
    
    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();

        activeQuests = GameObject.Find("ActiveQuests").GetComponent<TextMeshProUGUI>();

        foreach (string completedQuest in playerQuests.completedQuestNames)
        {
            tempDict.Add(completedQuest, 0);
        }
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
        if (playerTutorial.tutorialProgress >= 3)
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
                    else
                    {
                        if (!tempDict.ContainsKey(playerQuests.questList.questSlots[i].questName))
                        {
                            tempDict.Add(playerQuests.questList.questSlots[i].questName, 0);
                            playerQuests.completedQuestNames.Add(playerQuests.questList.questSlots[i].questName);
                            playerQuests.completedQuestDescs.Add(playerQuests.questList.questSlots[i].questDescription);
                        }
                        questSlots[i].questDescriptionText.text = "Report back to the villager that gave the quest!";
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
