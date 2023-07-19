using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EG1311QuestComplete : MonoBehaviour
{
    public CompleteCEGQuest completeCEGQuest;
    private PlayerQuests playerQuest;
    // Start is called before the first frame update
    private void Start()
    {
        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Eg1311Bullet"))
        {
            completeCEGQuest.shotLanded = true;
        }
    }
}
