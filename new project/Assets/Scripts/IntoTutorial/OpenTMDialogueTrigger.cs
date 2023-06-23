using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenTMDialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialText.text = "Please talk to the guy wearing the hat first.";
        }
    }
}
