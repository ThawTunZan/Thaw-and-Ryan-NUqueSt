using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BedSleep : MonoBehaviour
{
    int SceneIndx = 3;
    public GameObject Message;
    public Button yesButton;
    public Button noButton;

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
        DataPersistenceManager.instance.SaveGame();
       // data.playerPosition = new Vector3(0.6816905f, 0.6025486f, 0);
    }

    public void HideMessage()
    {
        Message.SetActive(false);
    }
}
