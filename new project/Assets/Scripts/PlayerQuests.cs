using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuests : MonoBehaviour, IDataPersistence
{
    public QuestList questList;
    public PlayerPositionSO startingPosition;

    public int cs1010Progress;
    public int cs1231Progress;
    public int cs2030Progress;
    public int cs2040Progress;
    public List<Vector2Int> cs2040SeenBefore = new List<Vector2Int>();

    public int ma1511Progress;

    public int eg1311Progress;
    public int cg2111aProgress;

    public List<int> dtk1234Collected = new List<int>() { 0, 0, 0, 0 };

    public List<string> completedQuestNames = new List<string>();
    public List<string> completedQuestDescs = new List<string>();

    private void Start()
    {
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            questList = new QuestList(6);
            questList = GameManager.instance.questList;

            cs1010Progress = GameManager.instance.cs1010Progress;
            cs1231Progress = GameManager.instance.cs1231Progress;
            cs2030Progress = GameManager.instance.cs2030Progress;
            cs2040Progress = GameManager.instance.cs2040Progress;
            cs2040SeenBefore = GameManager.instance.cs2040SeenBefore;

            ma1511Progress = GameManager.instance.ma1511Progress;

            eg1311Progress = GameManager.instance.eg1311Progress;
            cg2111aProgress = GameManager.instance.cg2111aProgress;

            dtk1234Collected = GameManager.instance.dtk1234Collected;

            completedQuestNames = GameManager.instance.completedQuestNames;
            completedQuestDescs = GameManager.instance.completedQuestDescs;
        }
        CheckQuestProgress();
    }

    /*
     * Certain scenes will look different if specific quests are completed. For example, in Cave_1a, both doors will be opened and
     * the wall puzzle will be solved whenever the player transits to that scene if they have already completed the CS1010 puzzle.
     */
    private void CheckQuestProgress()
    {
        string currScene = SceneManager.GetActiveScene().name;
        if (currScene == "Cave_1")
        {
            AllowEntryIfQuestStarted("CS1010", cs1010Progress, "CS1010Cover");
        }
        else if (currScene == "Cave_1a") // For CS1010
        {
            AllowEntryIfQuestStarted("CS1231", cs1231Progress, "CS1231Cover");
            PuzzleDoor puzzle1 = GameObject.Find("PuzzleDoor1").GetComponent<PuzzleDoor>();
            ForLoopPuzzle puzzle2 = GameObject.Find("WallPuzzleTrigger").GetComponent<ForLoopPuzzle>();
            puzzle1.CheckDoorsAtStart(cs1010Progress);
            puzzle2.CheckQuestProgress(cs1010Progress);
        }
        else if (currScene == "Cave_2a") // For CS1231
        {
            AllowEntryIfQuestStarted("CS2030", cs2030Progress, "CS2030Cover");
            ImplicationLogicPuzzle puzzle1 = GameObject.Find("WallPuzzleTrigger").GetComponent<ImplicationLogicPuzzle>();
            IfAndOnlyIfLogicPuzzle puzzle2 = GameObject.Find("WallPuzzleTrigger2").GetComponent<IfAndOnlyIfLogicPuzzle>();
            puzzle1.CheckQuestProgress(cs1231Progress);
            puzzle2.CheckQuestProgress(cs1231Progress);
        }
        else if (currScene == "Cave_3a") // For CS2030
        {
            AllowEntryIfQuestStarted("CS2040", cs2040Progress, "CS2040Cover");
            ClassInheritancePuzzle puzzle1 = GameObject.Find("WallPuzzleTrigger").GetComponent<ClassInheritancePuzzle>();
            puzzle1.CheckQuestProgress(cs2030Progress);
        }
        else if (currScene == "Cave_4a") // For CS2040
        {
            NQueensPuzzle puzzle1 = GameObject.Find("WallPuzzleTrigger").GetComponent<NQueensPuzzle>();
            puzzle1.CheckQuestProgress(cs2040Progress);
        }
        else if (currScene == "Village_WeaponShop") // For MA1511
        {
            if (ma1511Progress >= 2)
            {
                GameObject.Find("Slime (1)").SetActive(false);
            }
            if (ma1511Progress >= 1)
            {
                GameObject.Find("Slime").SetActive(false);
            }
            if (ma1511Progress >= 0)
            {
                GameObject.Find("MA1511Collider").SetActive(false);
            }
        }
        else if (currScene == "DCave_1") // For EG1311
        {
            if (eg1311Progress == 0 || cg2111aProgress == 0)
            {
                GameObject.Find("EG1311Collider").SetActive(false);
            }
        }
        else if (currScene == "DCave_1a") // For CG2111A
        {
            if (cg2111aProgress == 0)
            {
                GameObject.Find("CG2111ACollider").SetActive(false);
            }
        }
        else if (currScene == "ArtistHouse") // For DTK1234
        {
            GameObject.Find("DTK1234Quest").GetComponent<DTK1234Quest>().ChangeActive(dtk1234Collected);
        }
    }

    private bool SearchForQuest(string questName)
    {
        for (int i = 0; i < 6; i++)
        {
            if (questList.questSlots[i].questName == questName)
            {
                return true;
            }
        }
        return false;
    }

    private void AllowEntryIfQuestStarted(string questName, int questProgress, string colliderName)
    {
        if (SearchForQuest(questName) && questProgress == -1)
        {
            questProgress = 0;
        }
        if (questProgress != -1)
        {
            GameObject collider = GameObject.Find(colliderName);
            collider.SetActive(false);
        }
    }

    private void Update()
    {
        GameManager.instance.questList = questList;

        GameManager.instance.cs1010Progress = cs1010Progress;
        GameManager.instance.cs1231Progress = cs1231Progress;
        GameManager.instance.cs2030Progress = cs2030Progress;
        GameManager.instance.cs2040Progress = cs2040Progress;
        GameManager.instance.cs2040SeenBefore = cs2040SeenBefore;

        GameManager.instance.ma1511Progress = ma1511Progress;

        GameManager.instance.eg1311Progress = eg1311Progress;
        GameManager.instance.cg2111aProgress = cg2111aProgress;

        GameManager.instance.dtk1234Collected = dtk1234Collected;

        GameManager.instance.completedQuestNames = completedQuestNames;
        GameManager.instance.completedQuestDescs = completedQuestDescs;
    }

    public void LoadData(GameData data)
    {
        questList = new QuestList(6);
        questList = data.questList;

        cs1010Progress = data.cs1010Progress;
        cs1231Progress = data.cs1231Progress;
        cs2030Progress = data.cs2030Progress;
        cs2040Progress = data.cs2040Progress;
        cs2040SeenBefore = data.cs2040SeenBefore;

        ma1511Progress = data.ma1511Progress;

        eg1311Progress = data.eg1311Progress;
        cg2111aProgress = data.cg2111aProgress;

        dtk1234Collected = data.dtk1234Collected;

        completedQuestNames = data.completedQuestNames;
        completedQuestDescs = data.completedQuestDescs;
    }

    public void SaveData(GameData data)
    {
        data.questList = questList;

        data.cs1010Progress = cs1010Progress;
        data.cs1231Progress = cs1231Progress;
        data.cs2030Progress = cs2030Progress;
        data.cs2040Progress = cs2040Progress;
        data.cs2040SeenBefore = cs2040SeenBefore;

        data.ma1511Progress = ma1511Progress;

        data.eg1311Progress = eg1311Progress;
        data.cg2111aProgress = cg2111aProgress;

        data.dtk1234Collected = dtk1234Collected;

        data.completedQuestNames = completedQuestNames;
        data.completedQuestDescs = completedQuestDescs;
    }
}
