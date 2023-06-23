using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownMayorHouse_UI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private bool openTMDialogueDone;
    public GameObject dialoguePanel;

    private bool progressTMDialogueDone;

    private bool openSecDialogueDone;

    private void Update()
    {
        if (!openTMDialogueDone)
        {
            OpenTMDialogueCheck();
        }
        else if (!progressTMDialogueDone)
        {
            ProgressTMDialogueCheck();
        }
        else if (!openSecDialogueDone)
        {
            OpenSecDialogueCheck();
        }
    }

    private void OpenTMDialogueCheck()
    {
        if (dialoguePanel.activeSelf)
        {
            openTMDialogueDone = true;
            tutorialText.text = "Press SPACEBAR to progress the dialogue";
        }
    }

    private void ProgressTMDialogueCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            progressTMDialogueDone = true;
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }

    private void OpenSecDialogueCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            tutorialText.text = "Meet the secretary in the living room";
            tutorialPanel.SetActive(true);
        }
        else if (dialoguePanel.activeSelf)
        {
            openSecDialogueDone = true;
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }
}
