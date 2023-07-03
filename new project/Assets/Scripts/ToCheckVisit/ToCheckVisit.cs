using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToCheckVisit : MonoBehaviour
{
    public string questName;
    private string currLocation;
    private PlayerQuests playerQuests;
    private Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        currLocation = scene.name;
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (QuestList.QuestSlot questSlot in playerQuests.questList.questSlots)
            {
                // if quest is not done and it is the correct quest
                if (questSlot.questName == questName && questSlot.done == false)
                {
                    questSlot.placesToVisit.Remove(currLocation);
                }
            }
        }
    }
}
