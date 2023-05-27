using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (questSlot.questName == "MA1511")
        {
            if (questSlot.slimesRequired <= 0)
            {
                questSlot.done = true;
                questStatus.SetActive(true);
            }
        }
    }
}
