using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPuzzleTrigger : MonoBehaviour
{
    public GameObject wallPuzzle;
    public GameObject lockedWall;
    public ForLoopPuzzle forLoopPuzzle;

    private PlayerQuests playerQuests;

    bool playerInRange;

    private void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    void Update()
    {
        if (playerInRange && playerQuests.cs1010Progress < 2 && !forLoopPuzzle.inBattle && !forLoopPuzzle.spawnSlime)
        {
            lockedWall.SetActive(true);
            wallPuzzle.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
