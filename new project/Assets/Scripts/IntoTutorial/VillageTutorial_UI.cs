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

    public bool reachedFarmHouse;

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
            StartTutorialPart1();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void StartTutorialPart1()
    {
        if (reachedFarmHouse)
        {
            tutorialProgress = 2;
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
