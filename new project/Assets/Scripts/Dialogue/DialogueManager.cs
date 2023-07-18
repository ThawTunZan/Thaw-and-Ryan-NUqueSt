using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Story = Ink.Runtime.Story;
using Choice = Ink.Runtime.Choice;
using UnityEngine.EventSystems;
using UnityEditor;
using JetBrains.Annotations;
//using Ink.UnityIntegration;

public class DialogueManager : MonoBehaviour, IDataPersistence
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    private bool dialogueIsPlaying;

    private static DialogueManager instance;

    public PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private DialogueVariables dialogueVariables;
    private PlayerQuests player;

    public Inventory inventory;
    public Inventory toolbar;

    public bool openShop;

    public string localNPCName;
    private PlayerMoney playerMoney;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
            return;
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;

        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        inventory = GameObject.Find("Player").GetComponent<PlayerItems>().inventory;
        toolbar = GameObject.Find("Player").GetComponent<PlayerItems>().toolbar;
        playerMoney = GameObject.Find("Player").GetComponent<PlayerMoney>();

    }

    private void Update()
    {
        if (dialogueIsPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContinueStory();
            }
        }
    }
    
    public void CheckDate()
    {
        float currDay = float.Parse(currentStory.variablesState["currDay"].ToString());
        string questIsDone = currentStory.variablesState[localNPCName + "QuestDone"].ToString();
        if (GameManager.instance.day != currDay)
        {
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", false);
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
        }
        if (GameManager.instance.day == currDay && (questIsDone == "True" ||questIsDone == "true"))
        {

        }
        else if (GameManager.instance.day != currDay && (questIsDone == "True" || questIsDone == "true"))
        {
            dialogueVariables.InkSetVariables(currentStory, "currDay", GameManager.instance.day);
        }
        else
        {
            print("is valid time");
            dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
            dialogueVariables.InkSetVariables(currentStory, "currDay", GameManager.instance.day);
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        playerMovement.enabled = false;
        playerItems.disableToolbar = true;
        dialogueVariables.StartListening(currentStory);
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        if (localNPCName != "")
        {
            CheckDate();
            currentStory.BindExternalFunction("QuestCompleted", QuestCompleted);
            for (int i = 0; i < 5; i++)
            {
                if (player.questList.questSlots[i].questName == currentStory.variablesState[localNPCName + "QuestName"].ToString()
                    && player.questList.questSlots[i].questName != "")
                {
                    string questSTARTEDLOLOL = currentStory.variablesState[localNPCName + "QuestStarted"].ToString();
                    //to make questStarted false and questDone true when quest is completed
                    if (player.questList.questSlots[i].questName != "" && QuestIsDone(i) && (questSTARTEDLOLOL != "false" && questSTARTEDLOLOL != "False"))
                    {
                        // finished the quest 
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", true);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestStarted", false);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
                    }
                    else if (!QuestIsDone(i))
                    {
                        // quest is not finished while having it
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestDone", false);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "QuestStarted", true);
                        dialogueVariables.InkSetVariables(currentStory, localNPCName + "ValidTime", true);
                    }
                }
            }
        }
        else
        {
            currentStory.BindExternalFunction("QuestCompleted", QuestCompleted);
        }
        ContinueStory();
    }

    public void QuestCompleted()
    {
        for (int i = 0; i < 5; i += 1)
        {
            if (player.questList.questSlots[i].questName == currentStory.variablesState[localNPCName + "QuestName"].ToString()
                && player.questList.questSlots[i].questName != "")
            {
                player.questList.questSlots[i].questName = "";
                player.questList.questSlots[i].questDescription = "";
                player.questList.questSlots[i].done = false;
                player.questList.questSlots[i].count = 0;
                playerMoney.money += (int)player.questList.questSlots[i].gpaReward;
                Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                quest_UI.questSlots[i].GetComponent<QuestSlot_UI>().questStatus.SetActive(false);
            }
        }
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);
        currentStory.UnbindExternalFunction("QuestCompleted");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        playerItems.disableToolbar = false;
        playerMovement.enabled = true;
        DataPersistenceManager.instance.gameData.placeHolderStory = dialogueVariables.saveVariables();
    }

    private bool QuestIsDone(int x)
    {
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        // if there are no required items needed to pass to NPC return true
        if (player.questList.questSlots[x].done && player.questList.questSlots[x].questName != "")
        {
            return true;
        }
        return false;
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string currentLine = currentStory.Continue();
            dialogueText.text = currentLine;
            if (currentLine.Length < 2)
            {
                ContinueStory();
            }
            else if (currentLine.StartsWith("Great!") && localNPCName != "")
            {
                // referencing dictionary in DialogueVariables script which references variables from globals.ink file
                string questName = currentStory.variablesState[localNPCName + "QuestName"].ToString();
                string questDescription = currentStory.variablesState[localNPCName + "QuestDesc"].ToString();

                // add quest to Quest List under Player Quests component in Player via QuestList script
                player = GameObject.Find("Player").GetComponent<PlayerQuests>();
                player.questList.Add(questName, questDescription);
            }
            else if (currentLine.StartsWith("Sure. This is what we have in stock."))
            {
                openShop = true;
            }
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
    
    public void SaveData(GameData data)
    {
    }

    public void LoadData(GameData data)
    {
    }
}
