using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkToBlacksmithFirstTrigger : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialText.text = "Please visit the blacksmith at west of the village first.";
        }
    }
}
