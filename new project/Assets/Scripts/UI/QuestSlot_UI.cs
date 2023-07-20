using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestSlot_UI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;
    public Image questNPCImage;
    public Scrollbar scrollbar;
    public GameObject questStatus;

    public void SetItem(QuestList.QuestSlot questSlot)
    {
        if (questSlot != null)
        {
            questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, -330.06f);

            questNameText.text = questSlot.questName;
            questDescriptionText.text = questSlot.questDescription;
            questNPCImage.sprite = Resources.Load<Sprite>("Quest/" + questSlot.questNPCName);
            questNPCImage.color = new Color(1, 1, 1, 1);

            questDescriptionText.text += "\n\nGPA Reward: " + questSlot.gpaReward;

            LayoutRebuilder.ForceRebuildLayoutImmediate(questDescriptionText.rectTransform);
            Canvas.ForceUpdateCanvases();
            scrollbar.value = 1f;
            float textLength = questDescriptionText.textBounds.size.y;
            if (textLength <= 225)
            {
                questDescriptionText.rectTransform.offsetMin = new
                Vector2(questDescriptionText.rectTransform.offsetMin.x, 0);
            }
            else
            {
                float panelLength = 550.06f;
                questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, -330.06f + panelLength - textLength - 4);
            }
        }
    }

    public void SetEmpty()
    {
        questDescriptionText.rectTransform.offsetMin = new
                    Vector2(questDescriptionText.rectTransform.offsetMin.x, 0f);
        questNameText.text = "";
        questDescriptionText.text = "";
        questNPCImage.sprite = null;
        questNPCImage.color = new Color(1, 1, 1, 0);
    }

    // add quests completion requirement here
    public void QuestHandler(QuestList.QuestSlot questSlot)
    {
        PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        Inventory inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        Inventory toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        if (questSlot.done == true)
        {
            questStatus.SetActive(true);
        }
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
        if (questSlot.questName == "HSA1000" || questSlot.questName == "PC1101" || questSlot.questName == "HSI1000"
            || questSlot.questName == "HSS1000")
        {
            if (questSlot.placesToVisit.Count == 0)
            {
                questSlot.done = true;
                questStatus.SetActive(true);
            }
        }
        if (questSlot.questName == "DTK1234")
        {
            bool check = false;
            for (int i = 0; i < 21; i++)
            {
                if (inventory.slots[i].itemName == questSlot.questItemRequired && inventory.slots[i].count >= questSlot.questItemAmount)
                {
                    if (questSlot.questName == "DTK1234")
                    {
                        playerQuests.dtk1234Collected[0] = 0;
                    }
                    questSlot.done = true;
                    questStatus.SetActive(true);
                    check = true;
                    break;
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (toolbar.slots[i].itemName == questSlot.questItemRequired && toolbar.slots[i].count >= questSlot.questItemAmount)
                {
                    if (questSlot.questName == "DTK1234")
                    {
                        playerQuests.dtk1234Collected[0] = 0;
                    }
                    questSlot.done = true;
                    questStatus.SetActive(true);
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                questSlot.done = false;
                questStatus.SetActive(false);
            }
        }
    }

    public void RemoveItemFromPlayer(string itemName, int amountToRemove)
    {
        if (itemName == "")
        {
            return;
        }

        Inventory inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        Inventory toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        for (int i = 0; i < 21; i++)
        {
            if (inventory.slots[i].itemName == itemName)
            {
                inventory.Remove(i, amountToRemove);
                GameObject.Find("Inventory").GetComponent<Inventory_UI>().Refresh();
                return;
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (toolbar.slots[i].itemName == itemName)
            {
                toolbar.Remove(i, amountToRemove);
                GameObject.Find("Inventory").GetComponent<Inventory_UI>().Refresh();
                return;
            }
        }
    }
}
