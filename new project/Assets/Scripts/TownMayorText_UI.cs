using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownMayorText_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;

    private PlayerTutorial playerTutorial;
    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private int curr;
    private bool done;

    private void Start()
    {
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        playerItems.disableToolbar = true;
        playerMovement.enabled = false;
    }

    private void Update()
    {
        if (playerTutorial.tutorialProgress >= 1)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curr++;
        }
        if (curr == 0)
        {
            text.text = "You feel a strange warmth on your back, as if you were sleeping on a bed.";
        }
        else if (curr == 1)
        {
            text.text = "Your head hurts. You don't remember anything.";
        }
        else if (curr == 2)
        {
            text.text = "You get up anyway.";
        }
        else if (!done)
        {
            background.SetActive(false);
            text.gameObject.SetActive(false);
            button.SetActive(false);
            playerItems.disableToolbar = false;
            playerMovement.enabled = true;
            done = true;
        }
    }

    public void ContinueButton()
    {
        curr++;
    }
}
