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

    public PlayerMovement movement;
    float original_speed;

    private DialogueVariables dialogueVariables;
    private PlayerQuests player;

    public Inventory inventory;
    public Inventory toolbar;



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
        
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (movement.movespeed != 0)
        {
            original_speed = movement.movespeed;
        }
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
        movement.movespeed = 0;

        dialogueVariables.StartListening(currentStory);
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        CheckDate();

        for (int i = 0; i < 5; i++)
        {
            if (player.questList.questSlots[i].questName == currentStory.variablesState["questName"].ToString()
                && player.questList.questSlots[i].questName != "")
            {
                string questSTARTEDLOLOL = currentStory.variablesState["questStarted"].ToString();
                if (QuestIsDone(i) && (questSTARTEDLOLOL != "false" && questSTARTEDLOLOL != "False"))
                {
                    print(i);
                    dialogueVariables.InkSetVariables(currentStory, "questDone", player.questList.questSlots[i].done);
                    dialogueVariables.InkSetVariables(currentStory, "quest" + player.questList.questSlots[i].questName + "Done", player.questList.questSlots[i].done);
                    dialogueVariables.InkSetVariables(currentStory, "questStarted", false);
                }
                else
                {
                    dialogueVariables.InkSetVariables(currentStory, "questDone", false);
                    dialogueVariables.InkSetVariables(currentStory, "quest" + player.questList.questSlots[i].questName + "Done", false);
                    dialogueVariables.InkSetVariables(currentStory, "questStarted", true);
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
        movement.movespeed = original_speed;
        DataPersistenceManager.instance.gameData.placeHolderStory = dialogueVariables.saveVariables();
    }

    private bool QuestIsDone(int x)
    {
        player = GameObject.Find("Player").GetComponent<PlayerQuests>();
        // if there are no require items needed to pass to NPC return true
        if (player.questList.questSlots[x].requireItems.Count > 0 || player.questList.questSlots[x].slimesRequired > 0)
        {
            return false;
        }
        return true;
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
