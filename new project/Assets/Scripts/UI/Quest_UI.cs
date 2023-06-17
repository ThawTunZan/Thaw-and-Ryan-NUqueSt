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

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();

    private FreezePlayerMovement freezePlayerMovement;

    private Settings_UI settingsUI;

    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();
        settingsUI = GameObject.Find("Settings").GetComponent<Settings_UI>();
    }

    void Update()
    {
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Q))
        {
            questPanel.SetActive(true);
            playerItems.disableToolbar = true;
            freezePlayerMovement.ToggleMovement();
            Setup();
        }
        else if (playerItems.disableToolbar && !settingsUI.settingsActive 
            && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape)))
        {
            questPanel.SetActive(false);
            playerItems.disableToolbar = false;
            freezePlayerMovement.ToggleMovement();
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
