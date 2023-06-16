using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class GameButtons : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    private void Start()
    {

       // if (!DataPersistenceManager.instance.HasGameData())
       // {
       //     continueGameButton.interactable = false;
      //  }

    }
    public void NewGame()
    {
        DisableMenuButtons();
        //create new game instance
        DataPersistenceManager.instance.NewGame();
        //SceneManager.LoadSceneAsync("PlayerHouse");
        SceneManager.LoadSceneAsync("Cave_1");
    }

    public void ContinueGame()
    {
        DisableMenuButtons();
        //DataPersistenceManager.instance.LoadGame();
        SceneManager.LoadSceneAsync("PlayerHouse");
        //DataPersistenceManager.instance.LoadGame();
    }

    //disable so that player cant click continue button if there is no saved data
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}
