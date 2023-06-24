using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FarmHouseTutorial_UI : MonoBehaviour, IDataPersistence
{
    public int tutorialProgress;
    public PlayerPositionSO startingPosition;

    public TextMeshProUGUI tutorialText;

    public GameObject saveFirst1;
    public GameObject saveFirst2;

    private bool openedChest;
    public GameObject chestPanel;

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
            TutorialPart1();
        }
        else
        {
            Destroy(saveFirst1);
            Destroy(saveFirst2);
            Destroy(this.gameObject);
        }
    }

    void TutorialPart1()
    {
        if (!openedChest)
        {
            OpenedChestCheck();
        }
    }

    void OpenedChestCheck()
    {
        if (chestPanel.activeSelf)
        {
            openedChest = true;
            saveFirst2.SetActive(true);
            tutorialText.text = "Sleeping saves the game";
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
