using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponShopTutorial_UI : MonoBehaviour, IDataPersistence
{
    public int tutorialProgress;
    public PlayerPositionSO startingPosition;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    public GameObject dialoguePanel;

    public GameObject finishedWeaponShop;

    public GameObject questPanel;

    private bool startedTalking;

    private bool finishedTalking;

    private bool openedQuestList;

    public PlayerMovement playerMovement;

    void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            tutorialProgress = GameManager.instance.tutorialProgress;
        }
    }

    void Update()
    {
        GameManager.instance.tutorialProgress = tutorialProgress;
        if (tutorialProgress == 1)
        {
            TutorialPart1();
        }
        else if (tutorialProgress == 2)
        {
            Destroy(finishedWeaponShop);
            TutorialPart2();
        }
        else
        {
            Destroy(finishedWeaponShop);
            Destroy(this.gameObject);
        }
    }

    void TutorialPart1()
    {
        if (!startedTalking)
        {
            StartedTalkingCheck();
        }
        else if (!finishedTalking)
        {
            FinishedTalkingCheck();
        }
    }

    void StartedTalkingCheck()
    {
        tutorialText.text = "Have a chat with the blacksmith";
        if (dialoguePanel.activeSelf)
        {
            startedTalking = true;
            tutorialText.text = "";
            tutorialPanel.SetActive(false);
        }
    }

    void FinishedTalkingCheck()
    {
        if (!dialoguePanel.activeSelf)
        {
            finishedTalking = true;
            tutorialText.text = "Press Q to open up your quest list";
            tutorialPanel.SetActive(true);
            playerMovement.enabled = false;
            tutorialProgress = 2;
            Destroy(finishedWeaponShop);
        }
    }

    void TutorialPart2() 
    { 
        if (!openedQuestList)
        {
            OpenedQuestListCheck();
        }
    }

    void OpenedQuestListCheck()
    {
        if (questPanel.activeSelf)
        {
            openedQuestList = true;
            tutorialText.text = "Head south of the village to see your house";
        }
    }

    public void LoadData(GameData data)
    {
        tutorialProgress = data.tutorialProgress;
    }

    public void SaveData(GameData data)
    {
        data.tutorialProgress = tutorialProgress;
    }
}
