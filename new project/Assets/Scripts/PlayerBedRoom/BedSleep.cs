using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BedSleep : MonoBehaviour
{
    public GameObject Message;
    public Button yesButton;
    public Button noButton;

    public GameObject globalVolume;
    private ClockManager clockManager;
    public Animator transition;

    private void Start()
    {
        clockManager = globalVolume.GetComponent<ClockManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Message.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Message.SetActive(false);
    }

    public void SaveData()
    {
        clockManager.days += 1;
        DataPersistenceManager.instance.SaveGame();
        GameManager.instance.health = 100;
        GameManager.instance.inventory = DataPersistenceManager.instance.gameData.inventory;
        GameManager.instance.toolbar = DataPersistenceManager.instance.gameData.toolbar;
        GameManager.instance.day = DataPersistenceManager.instance.gameData.day;


        GoToSleep();
    }
    
    private void LoadGameData()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    public void HideMessage()
    {
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

        // DataPersistenceManager.instance.LoadGame();
        transition.Play("Base Layer.PlayerFaintEnd",0 ,0);

       // SceneManager.LoadScene("PlayerHouse", LoadSceneMode.Single);
       // DataPersistenceManager.instance.LoadGame();
    }
}
