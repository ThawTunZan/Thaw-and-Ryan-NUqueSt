using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Quest_UI : MonoBehaviour
{
    public GameObject questPanel;

    public PlayerQuests player;

    public List<QuestSlot_UI> questSlots = new List<QuestSlot_UI>();

    public PlayerMovement movement;

    float original_speed;

    private void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
            original_speed = movement.movespeed;
            movement.movespeed = 0;
            Setup();
        }
        else
        {
            questPanel.SetActive(false);
            movement.movespeed = original_speed;
        }
    }

    void Setup()
    {
        if (questSlots.Count == player.questList.questSlots.Count)
        {
            for (int i = 0; i < questSlots.Count; i++)
            {
                if (player.questList.questSlots[i].count == 1)
                {
                    questSlots[i].SetItem(player.questList.questSlots[i]);
                }
                else
                {
                    questSlots[i].SetEmpty();
                }
            }
        }
    }
}
