using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHouseTutorial_UI : MonoBehaviour
{
    private int tutorialProgress;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    public GameObject saveFirst1;
    public GameObject saveFirst2;

    private bool openedChest;
    public GameObject chestPanel;

    private bool readNote;
    public GameObject note;
    public GameObject notePanel;

    private bool nearBed;
    public GameObject bedSleepPanel;

    void Update()
    {
        GameManager.instance.tutorialProgress = tutorialProgress;
        if (GameManager.instance.tutorialProgress == 2)
        {
            TutorialPart1();
        }
        else if (GameManager.instance.tutorialProgress == 3)
        {
            Destroy(saveFirst2);
            TutorialPart2();
        }
        else
        {
            Destroy(saveFirst1);
            Destroy(saveFirst2);
            Destroy(this.gameObject);
        }
    }

    void TutorialPart1()
    {
        if (!openedChest)
        {
            OpenedChestCheck();
        }
        else if (!nearBed)
        {
            NearBedCheck();
        }
    }

    void OpenedChestCheck()
    {
        if (chestPanel.activeSelf)
        {
            openedChest = true;
            saveFirst2.SetActive(true);
            tutorialText.text = "Sleeping saves the game";
        }
    }

    void NearBedCheck()
    {
        if (bedSleepPanel.activeSelf)
        {
            nearBed = true;
        }
    }

    void TutorialPart2()
    {
        if (!readNote)
        {
            ReadNoteCheck();
        }
        else
        {
            CloseNoteCheck();
        }
    }

    void ReadNoteCheck()
    {
        note.SetActive(true);
        tutorialText.text = "Press E to read the note on the table";
        if (notePanel.activeSelf)
        {
            readNote = true;
        }
    }

    void CloseNoteCheck()
    {
        if (!notePanel.activeSelf)
        {
            tutorialProgress = 4;
        }
    }
}
