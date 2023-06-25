using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    private PlayerQuests playerQuests;
    private PlayerItems playerItems;
    private PlayerMovement playerMovement;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();
    
    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Setup();
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Q))
        {
            questPanel.SetActive(true);
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && questPanel.activeSelf && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)))
        {
            questPanel.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
        }
    }

    void Setup()
    {
        if (questSlots.Count == playerQuests.questList.questSlots.Count)
        {
            for (int i = 0; i < questSlots.Count; i++)
            {
                if (playerQuests.questList.questSlots[i].count == 1)
                {
                    questSlots[i].SetItem(playerQuests.questList.questSlots[i]);
                }
                else
                {
                    questSlots[i].SetEmpty();
                }
            }
        }
    }
}
