using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    public GameObject Lever1Up;
    public GameObject Lever1Down;
    public GameObject Lever2Up;
    public GameObject Lever2Down;
    public GameObject Lever3Up;
    public GameObject Lever3Down;
    public GameObject Lever4Up;
    public GameObject Lever4Down;
    public GameObject Lever5Up;
    public GameObject Lever5Down;
    public GameObject Door1;
    public GameObject Door2;

    private PlayerQuests playerQuests;

    void Start()
    {
        playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
        CheckDoorsAtStart(playerQuests.cs1010Progress);
    }

    void Update()
    {
        CheckDoor1();
        CheckDoor2();
    }

    private void CheckDoorsAtStart(int progress)
    {
        if (progress > 0)
        {
            Lever1Up.SetActive(false);
            Lever1Down.SetActive(true);
            Lever2Up.SetActive(false);
            Lever2Down.SetActive(true);
            Door1.SetActive(false);
        }
        if (progress > 1)
        {
            Lever3Up.SetActive(false);
            Lever3Down.SetActive(true);
            Lever5Up.SetActive(false);
            Lever5Down.SetActive(true);
            Door2.SetActive(false);
        }
    }

    private void CheckDoor1()
    {
        if (Lever1Down.activeSelf && Lever2Down.activeSelf)
        {
            playerQuests.cs1010Progress = 1;
            Door1.SetActive(false);
        }
        else
        {
            playerQuests.cs1010Progress = 0;
            Door1.SetActive(true);
        }
    }

    private void CheckDoor2()
    {
        if ((Lever3Down.activeSelf && Lever5Down.activeSelf) ^ (Lever4Down.activeSelf && Lever5Down.activeSelf))
        {
            playerQuests.cs1010Progress = 2;
            Door2.SetActive(false);
        }
        else
        {
            playerQuests.cs1010Progress = 1;
            Door2.SetActive(true);
        }
    }
}
