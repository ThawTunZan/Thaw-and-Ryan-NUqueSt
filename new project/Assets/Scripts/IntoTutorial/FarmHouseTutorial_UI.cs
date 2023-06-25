using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmHouseTutorial_UI : MonoBehaviour, IDataPersistence
{
    public int tutorialProgress;
    public PlayerPositionSO startingPosition;

    public TextMeshProUGUI tutorialText;

    public GameObject enterPlayerHouseFirst;

    // Start is called before the first frame update
    void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            tutorialProgress = GameManager.instance.tutorialProgress;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.tutorialProgress = tutorialProgress;
        if (tutorialProgress == 2)
        {
            tutorialText.text = "Enter your house west of the path";
        }
        else
        {
            Destroy(enterPlayerHouseFirst);
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
