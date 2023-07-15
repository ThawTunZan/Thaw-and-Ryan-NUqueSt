using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class CompleteCg1111A : MonoBehaviour
{
    private PlayerQuests playerQuest;
    private PlayerMovement playerMovement;
    private CinemachineVirtualCamera virtualCamera;
    private RobotMovement robotMovement;
    private GameObject player;
    private bool allColorsDetected;

    public List<string> colorsDetected;
    // Start is called before the first frame update
    void Start()
    {
        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        robotMovement = GameObject.Find("Robot").GetComponent<RobotMovement>();
        player = GameObject.Find("Player");

        allColorsDetected = false;
        colorsDetected = new List<string>();

    }

    private void Update()
    {
        if (colorsDetected.Count == 4)
        {
            allColorsDetected = true;
        }
        else
        {
            allColorsDetected = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (allColorsDetected && playerQuest.questList.questSlots[i].questName == "CG1111A")
                {
                    playerQuest.questList.questSlots[i].done = true;
                    playerMovement.enabled = true;
                    robotMovement.enabled = false;
                    virtualCamera.Follow = player.transform;
                }
            }
        }
    }
}
