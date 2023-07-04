using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BedSleep : MonoBehaviour
{
    public PlayerTutorial playerTutorial;

    public GameObject Message;
    public Button yesButton;
    public Button noButton;

    public GameObject globalVolume;
    private ClockManager clockManager;
    public Animator transition;

    public PlayerHouseTutorial_UI tutorialUI;
    public PlayerItems playerItems;
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();
        clockManager = globalVolume.GetComponent<ClockManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerItems.disableToolbar = true;
            Message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerItems.disableToolbar = false;
            Message.SetActive(false);
        }
    }

    public void SaveData()
    {
        HideMessage();
        playerMovement.enabled = false;
        clockManager.days += 1;
        if (GameManager.instance.tutorialProgress == 2)
        {
            playerTutorial.tutorialProgress = 3;
        }
        DataPersistenceManager.instance.SaveGame();
        GameManager.instance.health = 100;
        GameManager.instance.inventory = DataPersistenceManager.instance.gameData.inventory;
        GameManager.instance.toolbar = DataPersistenceManager.instance.gameData.toolbar;
        GameManager.instance.day += 1;


        GoToSleep();
    }
    
    private void LoadGameData()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    public void HideMessage()
    {
        playerItems.disableToolbar = false;
        Message.SetActive(false);
    }

    public void GoToSleep()
    {
        StartCoroutine(WaitSleepAnimation());
    }

    IEnumerator WaitSleepAnimation()
    {
        transition.SetTrigger("Sleep");
        yield return new WaitForSeconds(5);
        playerMovement.enabled = true;
        transition.Play("Base Layer.PlayerFaintEnd", 0, 0);
        clockManager.hours = 8;
        clockManager.minutes = 0;
        clockManager.seconds = 0;
        GameManager.instance.hours = 8;
        GameManager.instance.minutes = 0;
        GameManager.instance.seconds = 0;
    }
}
