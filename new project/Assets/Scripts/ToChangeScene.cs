using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToChangeScene : MonoBehaviour
{
    public int indexOfScene;
    public Vector3 playerPosition;
    public PlayerPositionSO storePlayerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("GoBackHome collision is triggered");
        if (collision.CompareTag("Player"))
        {
            print("player has entered the GoBackHome collider, switching scene to FarmHouse");
            storePlayerPosition.InitialValue = playerPosition;
            storePlayerPosition.transittedScene = true;
            DataPersistenceManager.instance.sceneTransitted = true;
            SceneManager.LoadScene(indexOfScene, LoadSceneMode.Single);          
        }
    }
}
