using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallPuzzle : MonoBehaviour
{
    public GameObject puzzlePanel;
    public GameObject puzzleActivated;
    public GameObject puzzleCorrect;
    public GameObject puzzleText;
    public TMP_InputField puzzleInput;

    public GameObject visualCue;

    private PlayerItems playerItems;
    private PlayerMovement playerMovement;
    private PlayerQuests playerQuests;

    private bool playerInRange;
    private int randX;
    private int randA;
    private int randB;
    public int puzzleAnswer;

    public bool spawnSlime;
    public bool inBattle;
    public bool isDone;

    private void Start()
    {
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        randX = Random.Range(6, 15);
        randA = Random.Range(5, 10);
        randB = Random.Range(2, 5);
        HideUI();
    }

    private void Update()
    {
        if (playerInRange && puzzleActivated.activeSelf)
        {
            TriggerNote();
        }
        if (spawnSlime)
        {
            CheckInBattle();
        }
    }

    private void TriggerNote()
    {
        if (!playerItems.disableToolbar && !puzzlePanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            ChangePuzzleText();
            ShowUI();
            GetPuzzleAnswer();
            playerItems.disableToolbar = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && puzzlePanel.activeSelf
            && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            HideUI();
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            if (inBattle)
            {
                spawnSlime = true;
                puzzleActivated.SetActive(false);
                Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2((float)0.9, (float)-2.2), Quaternion.identity);
                Instantiate(EnemySpawner.instance.GetEnemyByName("Slime"), new Vector2(2, (float)-2.2), Quaternion.identity);
            }
        }
    }

    private void ChangePuzzleText()
    {
        TextMeshProUGUI randText = puzzleText.GetComponent<TextMeshProUGUI>();
        randText.text = "Solve the for loop to free yourself!" +
            "\n\nEvery wrong answer has consequences..." +
            "\n\nx = " + randX + ";" +
            "\nfor (int i = 0; i < " + randA + "; i++)" +
            "\n{" +
            "\n    x = x * " + randB + " + i;" +
            "\n}" +
            "\n\nWhat is the value of x after the for loop?";
    }

    private int GetPuzzleAnswer()
    {
        puzzleAnswer = randX;
        int a = randA;
        int b = randB;
        for (int i = 0; i < a; i++)
        {
            puzzleAnswer = puzzleAnswer * b + i;
        }
        return puzzleAnswer;
    }

    public void CheckAnswer()
    {
        bool parseSuccess = int.TryParse(puzzleInput.text.Trim(), out int playerAnswer);
        if (parseSuccess)
        {
            TextMeshProUGUI text = puzzleText.GetComponent<TextMeshProUGUI>();
            if (playerAnswer == GetPuzzleAnswer())
            {
                text.text = "Correct!\n\nYou are now freed from this room.";
                playerQuests.cs1010Progress = 2;
            }
            else
            {
                text.text = "Oh no, that is wrong...\n\nThere is a surprise waiting for you :)";
                inBattle = true;
            }
        }
    }

    private void CheckInBattle()
    {
        if (GameObject.Find("Slime(Clone)") == null)
        {
            inBattle = false;
            spawnSlime = false;
        }
    }

    private void ShowUI()
    {
        puzzleText.SetActive(true);
        puzzleInput.gameObject.SetActive(true);
        puzzlePanel.SetActive(true);
    }

    private void HideUI()
    {
        puzzlePanel.SetActive(false);
        puzzleText.SetActive(false);
        puzzleInput.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && puzzleActivated.activeSelf && !puzzleCorrect.activeSelf)
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }
}
