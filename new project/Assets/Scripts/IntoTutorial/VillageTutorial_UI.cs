using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageTutorial_UI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    public GameObject talkToBlacksmithFirst;

    public bool reachedBlacksmith;

    private void Update()
    {
        if (GameManager.instance.tutorialProgress == 1)
        {
            tutorialText.text = "Head west of the village to meet the blacksmith";
        }
        else if (GameManager.instance.tutorialProgress == 2)
        {
            Destroy(talkToBlacksmithFirst);
            tutorialText.text = "Head south of the village to see your house";
        }
        else
        {
            Destroy(talkToBlacksmithFirst);
            Destroy(this.gameObject);
        }
    }
}
