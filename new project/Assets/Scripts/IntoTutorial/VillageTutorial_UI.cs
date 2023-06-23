using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageTutorial_UI : MonoBehaviour, IDataPersistence
{
    public int tutorialProgress;
    public PlayerPositionSO startingPosition;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    public bool reachedBlacksmith;

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            tutorialProgress = GameManager.instance.tutorialProgress;
        }
    }

    private void Update()
    {
        GameManager.instance.tutorialProgress = tutorialProgress;
        if (GameManager.instance.tutorialProgress == 1)
        {
            tutorialText.text = "Head west of the village to meet the blacksmith";
        }
        else if (GameManager.instance.tutorialProgress == 2)
        {
            tutorialText.text = "Head south of the village to see your house";
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        tutorialProgress = data.tutorialProgress;
    }

    public void SaveData(GameData data)
    {
        data.tutorialProgress = tutorialProgress;
    }
}
