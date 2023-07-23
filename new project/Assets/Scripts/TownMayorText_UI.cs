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

    private int curr;

    private void Start()
    {
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();
    }

    public void Update()
    {
        if (playerTutorial.tutorialProgress == 1)
        {
            gameObject.SetActive(false);
        }
        if (curr == 0)
        {
            text.text = "You feel a strange warmth on your back, as if you were sleeping on a bed.";
        }
        else if (curr == 1)
        {
            text.text = "Your head hurts. You get up anyway.";
        }
        else
        {
            background.SetActive(false);
            text.gameObject.SetActive(false);
            button.SetActive(false);
        }
    }

    public void ContinueButton()
    {
        curr++;
    }
}
