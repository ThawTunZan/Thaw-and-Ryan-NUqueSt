using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestList
{
    [System.Serializable]
    public class QuestSlot
    {
        public int count;
        public int maxAllowed;
        public int temp;
        public string questName;
        public string questDescription;

        public QuestSlot()
        {
            count = 0;
            temp = 0;
            maxAllowed = 1;
        }

        public bool CanAddItem()
        {
            if (count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(string quest_name, string quest_description)
        {
            questName = quest_name;
            questDescription = quest_description;
            temp = 1;
            count++;
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
            if (questSlot.CanAddItem())
            {
                questSlot.AddItem(questName, questDescription);
                return;
            }
        }
    }
}
