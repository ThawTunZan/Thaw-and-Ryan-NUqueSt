using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;

public class QuestSlot_UI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;
    public GameObject questStatus;

    public void SetItem(QuestList.QuestSlot questSlot)
    {

        if (questSlot != null)
        {
            questNameText.text = questSlot.questName;
            questDescriptionText.text = questSlot.questDescription;
            QuestHandler(questSlot);
        }
    }

    public void SetEmpty()
    {
        questNameText.text = "";
        questDescriptionText.text = "";
    }

    // add quests completion requirement here
    public void QuestHandler(QuestList.QuestSlot questSlot)
    {
        PlayerQuests player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        Inventory inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        Inventory toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        if (questSlot.questName == "MA1511")
        {
            if (questSlot.slimesRequired <= 0)
            {
                questSlot.done = true;
                questStatus.SetActive(true);
            }
        }
        if (questSlot.questName == "MA1512" || questSlot.questName == "GEA1000")
        {
            foreach (Dictionary<string, int> questDict in questSlot.requireItems)
            {
                //to check inventory
                foreach (Inventory.Slot slot in inventory.slots)
                {
                    if (questDict.ContainsKey(""))
                    {
                        break;
                    }
                    //check if same item 
                    if (questDict.ContainsKey(slot.itemName) && questDict[slot.itemName] < slot.count)
                    {
                        slot.count -= questDict[slot.itemName];
                        questSlot.requireItems.Remove(questDict);
                        break;
                    }
                    else if (questDict.ContainsKey(slot.itemName) && questDict[slot.itemName] == slot.count)
                    {
                        questSlot.requireItems.Remove(questDict);
                        slot.icon = null;
                        slot.itemName = "";
                        //remove the item
                        break;
                    }
                }
                //to check toolbar
                foreach (Inventory.Slot slot in toolbar.slots)
                {
                    if (questDict.ContainsKey(""))
                    {
                        break;
                    }
                    //check for same item
                    if (questDict.ContainsKey(slot.itemName) && questDict[slot.itemName] < slot.count)
                    {
                        slot.count -= questDict[slot.itemName];
                        questSlot.requireItems.Remove(questDict);
                        //remove the item, add ltr
                        break;
                    }
                    else if (questDict.ContainsKey(slot.itemName) && questDict[slot.itemName] == slot.count)
                    {
                        questSlot.requireItems.Remove(questDict);
                        slot.icon = null;
                        slot.itemName = "";
                        //remove the item, add ltr
                        break;
                    }
                }
                if (questSlot.requireItems.Count == 0)
                {
                    print("requirements fulfilled");
                    questSlot.done = true;
                    questStatus.SetActive(true);
                }
            }
        }
        if (questSlot.questName == "HSA1000")
        {
            if (questSlot.placesToVisit.Count == 0)
            {
                questSlot.done = true;
                questStatus.SetActive(true);
            }
        }
    }
}
