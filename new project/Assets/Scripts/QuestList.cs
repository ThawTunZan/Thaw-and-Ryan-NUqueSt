using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UIElements;

[System.Serializable]
public class QuestList
{
    [System.Serializable]
    public class QuestSlot
    {
        public int count;
        public string questName;
        public string questNPCName;
        public string questDescription;
        public bool done;
        public float gpaReward;
        public bool testing;

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
                GameObject.Find("Player").GetComponent<PlayerQuests>().ma1511Progress = 0;
                GameObject.Find("MA1511Collider").SetActive(false);
                slimesRequired = 2;
                gpaReward = 10;
            }
            if (quest_name == "MA1512")
            {
                //requireItems = new List<Dictionary<string, int>>();
                requireItems.Add(new Dictionary<string, int> { { "Tomato", 2 } });
                gpaReward = 10;
            }
            if (quest_name == "MA1508E")
            {
                gpaReward = 20;
            }
            if (quest_name == "HSA1000")
            {
                placesToVisit.Add("Cave_1");
                gpaReward = 20;
            }
            if (quest_name == "GESS1001")
            {
                placesToVisit.Add("Cave_1b");
                gpaReward = 20;
            }
            if (quest_name == "GEA1000")
            {
                requireItems.Add(new Dictionary<string, int> { { "Iron Ore", 1 } });
                gpaReward = 20;
            }
            if (quest_name == "PC1101")
            {
                placesToVisit.Add("Village");
                gpaReward = 10;
            }
            if (quest_name == "PC1201")
            {
                gpaReward = 20;
            }
            if (quest_name == "CS1010")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS1231")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS2030")
            {
                gpaReward = 40;
            }
            if (quest_name == "CS2040")
            {
                gpaReward = 40;
            }
            if (quest_name == "CG1111A")
            {
                gpaReward = 30;
            }
            if (quest_name == "CG2111A")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().cg2111aProgress = 0;
                gpaReward = 40;
            }
            if (quest_name == "EG1311")
            {
                GameObject.Find("Player").GetComponent<PlayerQuests>().eg1311Progress = 0;
                gpaReward = 30;
            }
            if (testing == false)
            {
                questNPCName = DialogueManager.GetInstance().localNPCName;
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
