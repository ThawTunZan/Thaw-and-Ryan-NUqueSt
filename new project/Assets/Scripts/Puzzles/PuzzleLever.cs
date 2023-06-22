using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLever : MonoBehaviour
{
    public GameObject leverUp;
    public GameObject leverDown;
    public GameObject puzzleDoor;

    bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        if (leverUp.activeSelf)
        {
            leverUp.SetActive(false);
            leverDown.SetActive(true);
            puzzleDoor.SetActive(false);
        }
        else
        {
            leverUp.SetActive(true);
            leverDown.SetActive(false);
            puzzleDoor.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}