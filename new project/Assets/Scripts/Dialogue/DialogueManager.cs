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
        string questIsDone = currentStory.variablesState["questDone"].ToString();
        if (GameManager.instance.day != currDay)
        {
            dialogueVariables.InkSetVariables(currentStory, "questDone", false);
        }
        if (GameManager.instance.day == currDay && (questIsDone == "True" ||questIsDone == "true"))
        {
            print("not valid time");
            dialogueVariables.InkSetVariables(currentStory, "validTime", false);
        }
        else
        {
            print("is valid time");
            dialogueVariables.InkSetVariables(currentStory, "validTime", true);
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
        CheckDate();

        for (int i = 0; i < 5; i++)
        {
            if (player.questList.questSlots[i].questName == currentStory.variablesState["questName"].ToString()
                && player.questList.questSlots[i].questName != "")
            {
                string questSTARTEDLOLOL = currentStory.variablesState["questStarted"].ToString();
                //to make questStarted false and questDone true when quest is completed
                if (player.questList.questSlots[i].questName != "" && QuestIsDone(i) && (questSTARTEDLOLOL != "false" && questSTARTEDLOLOL != "False"))
                {
                    dialogueVariables.InkSetVariables(currentStory, "questDone", true);
                   // dialogueVariables.InkSetVariables(currentStory, "quest" + player.questList.questSlots[i].questName + "Done", true);
                    dialogueVariables.InkSetVariables(currentStory, "questStarted", false);
                    //dialogueVariables.InkSetVariables(currentStory, "validTime", false);
                    // remove the quest from quest slot
                    player.questList.questSlots[i].questName = "";
                    player.questList.questSlots[i].questDescription = "";
                    player.questList.questSlots[i].done = false;
                    Quest_UI quest_UI = GameObject.Find("Quest").GetComponent<Quest_UI>();
                    quest_UI.questSlots[i].GetComponent<QuestSlot_UI>().questStatus.SetActive(false);
                    player.questList.questSlots[i].count = 0;
                }
                else
                {
                    dialogueVariables.InkSetVariables(currentStory, "questDone", false);
                   // dialogueVariables.InkSetVariables(currentStory, "quest" + player.questList.questSlots[i].questName + "Done", false);
                    dialogueVariables.InkSetVariables(currentStory, "questStarted", true);
                    dialogueVariables.InkSetVariables(currentStory, "validTime", true);
                }
            }
        }
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);

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

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            string currentLine = currentStory.Continue();
            dialogueText.text = currentLine;
            if (currentLine.StartsWith("Great!"))
            {
                // referencing dictionary in DialogueVariables script which references variables from globals.ink file
                string questName = ((Ink.Runtime.StringValue) dialogueVariables.GetVariableState("questName")).value;
                string questDescription = ((Ink.Runtime.StringValue) dialogueVariables.GetVariableState("questDesc")).value;

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
