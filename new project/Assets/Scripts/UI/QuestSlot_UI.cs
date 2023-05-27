using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSlot_UI : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questDescriptionText;

    public void SetItem(QuestList.QuestSlot questSlot)
    {
        if (questSlot != null)
        {
            questNameText.text = questSlot.questName;
            questDescriptionText.text = questSlot.questDescription;
        }
    }

    public void SetEmpty()
    {
        questNameText.text = "";
        questDescriptionText.text = "";
    }
}
