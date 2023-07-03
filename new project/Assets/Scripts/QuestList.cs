using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;

[System.Serializable]
public class QuestList
{
    [System.Serializable]
    public class QuestSlot
    {
        public int count;
        public string questName;
        public string questDescription;
        public bool done;

        // add more requirements here for different quests
        public int slimesRequired;
        [SerializeField]public List<Dictionary<string, int>> requireItems = new List<Dictionary<string, int>>();
        public List<string> placesToVisit = new List<string>();

        public void AddItem(string quest_name, string quest_description)
        {
            questName = quest_name;
            questDescription = quest_description;
            QuestHandler(quest_name);
            count++;
        }

        // add more quests here
        public void QuestHandler(string quest_name)
        {
            if (quest_name == "MA1511")
            {
                slimesRequired = 2;
            }
            if (quest_name == "MA1512")
            {
                //requireItems = new List<Dictionary<string, int>>();
                requireItems.Add(new Dictionary<string, int> { { "Tomato", 2 } });
            }
            if (quest_name == "HSA1000")
            {
                placesToVisit.Add("Cave_1");
            }
            if (quest_name == "GESS1001")
            {
                placesToVisit.Add("Cave_1b");
            }
            if (quest_name == "GEA1000")
            {
                requireItems.Add(new Dictionary<string, int> { { "Iron Ore", 1 } });
            }
        }
    }

    public List<QuestSlot> questSlots = new List<QuestSlot>();

    public QuestList(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            QuestSlot questSlot = new QuestSlot();
            questSlots.Add(questSlot);
        }
    }

    public void Add(string questName, string questDescription)
    {
        foreach (QuestSlot questSlot in questSlots)
        {
            if (questSlot.count == 0)
            {
                questSlot.AddItem(questName, questDescription);
                return;
            }
        }
    }
}
