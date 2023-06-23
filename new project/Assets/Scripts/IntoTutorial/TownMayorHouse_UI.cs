using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class TownMayorHouse_UI : MonoBehaviour, IDataPersistence
{
    public bool tutorialTMHDone;
    public PlayerPositionSO startingPosition;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private bool openTMDialogueDone;
    public GameObject dialoguePanel;

    private bool progressTMDialogueDone;
    public GameObject openTMDialogueFirst;

    private bool openSecDialogueDone;

    private bool progressSecDialogueDone;
    public GameObject openSecDialogueFirst;

    private bool exitHouseDone;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            tutorialTMHDone = GameManager.instance.tutorialTMHDone;
        }
    }

    private void Update()
    {
        GameManager.instance.tutorialTMHDone = tutorialTMHDone;
        if (GameManager.instance.tutorialTMHDone)
        {
            Destroy(openTMDialogueFirst);
            Destroy(openSecDialogueFirst);
            Destroy(this.gameObject);
        }
        else
        {
            StartTutorial();
        }
    }

    private void StartTutorial()
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
        else if (!exitHouseDone)
        {
            ExitHouseCheck();
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

    private void ExitHouseCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            tutorialText.text = "Visit your house south of the village!";
            tutorialPanel.SetActive(true);
            Invoke(nameof(ChangeTutorialTMHDone), 1.5f);
        }
    }

    private void ChangeTutorialTMHDone()
    {
        tutorialTMHDone = true;
    }

    public void LoadData(GameData data)
    {
        tutorialTMHDone = data.tutorialTMHDone;
    }

    public void SaveData(GameData data)
    {
        data.tutorialTMHDone = tutorialTMHDone;
    }
}