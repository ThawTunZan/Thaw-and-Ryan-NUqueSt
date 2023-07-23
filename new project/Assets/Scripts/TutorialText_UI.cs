using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText_UI : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject button;

    private int curr;

    public void Update()
    {
        if (curr == 0)
        {
            text.text = "You were running for your life after being invaded by monsters.";
        }
        else if (curr == 1)
        {
            text.text = "After running for a while, you reach a dead end.";
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
