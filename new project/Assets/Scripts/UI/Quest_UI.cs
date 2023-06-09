using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    public PlayerQuests playerQuests;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();

    private FreezePlayerMovement freezePlayerMovement;

    private void Start()
    {
        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleQuestList();
        }
    }

    public void ToggleQuestList()
    {
        if (!questPanel.activeSelf)
        {
            questPanel.SetActive(true);
            freezePlayerMovement.ToggleMovement();
            Setup();
        }
        else
        {
            questPanel.SetActive(false);
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
