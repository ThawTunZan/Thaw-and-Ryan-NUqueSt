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
    public GameObject openTMDialogueFirst;

    private bool openSecDialogueDone;

    private bool progressSecDialogueDone;
    public GameObject openSecDialogueFirst;

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
        else if (!progressSecDialogueDone)
        {
            ProgressSecDialogueCheck();
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
            Destroy(openTMDialogueFirst);
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }

    private void OpenSecDialogueCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            openSecDialogueDone = true;
            tutorialText.text = "Meet the secretary in the living room";
            tutorialPanel.SetActive(true);
        }
    }

    private void ProgressSecDialogueCheck()
    {
        if (dialoguePanel.activeSelf)
        {
            progressSecDialogueDone = true;
            Destroy(openSecDialogueFirst);
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }
}
