using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlRobot : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CinemachineVirtualCamera virtualCamera;
    public RobotMovement robotMovement;
    public GameObject robot;
    public GameObject questPanel;
    public GameObject timerPanel;
    public float timer;
    public float givenTimer;

    public bool TimerOn;
    public CompleteCEGQuest questComplete;
    private PlayerQuests playerQuest;
    public string questName;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        virtualCamera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        robot = GameObject.Find("Robot");
        robotMovement = GameObject.Find("Robot").GetComponent <RobotMovement>();
        if (questName == "CG1111A")
        {
            timer = 45f;
        }
        else if (questName == "EG1311")
        {
            timer = 30f;
        }
        TimerOn = false;

        playerQuest = GameObject.Find("Player").GetComponent<PlayerQuests>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimerOn)
        {
            StartCountDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < 5; i++)
            {
                //if there is an active quest in the slot
                if (playerQuest.questList.questSlots[i].questName == questName && collision.gameObject.CompareTag("Player"))
                {
                    playerMovement.enabled = false;
                    OpenPanels();
                }
            }
        }
    }
    private void OpenPanels()
    {
        questPanel.SetActive(true);
    }

    public void YesPressed()
    {
        questPanel.SetActive(false);
        timerPanel.SetActive(true);
        robotMovement.enabled = true;
        virtualCamera.Follow = robot.transform;
        TimerOn = true;
        if (questName == "CG1111A" || questName == "CG2111A")
        {
            questComplete.colorsDetected.Clear();
        }
        playerMovement.movespeed = 0;
    }

    public void NoPressed()
    {
        questPanel.SetActive(false);
        playerMovement.enabled = true;
        robotMovement.enabled = false;
    }
    [SerializeField]
    private void StartCountDown()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            int roundedTimer = Mathf.FloorToInt(timer);
            TextMeshProUGUI timerText = timerPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            timerText.text = roundedTimer.ToString();
        }
        else
        {
            robot.transform.position = new Vector2(-1.933f, -0.352f);
            GameObject player = GameObject.Find("Player");
            playerMovement.enabled = true;
            robotMovement.enabled = false;
            virtualCamera.Follow = player.transform;
            TimerOn = false;
            timer = givenTimer;
            timerPanel.SetActive(false);
            playerMovement.movespeed = 1f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerMovement.enabled = true;
        questPanel.SetActive(false);
        playerMovement.movespeed = 1f;
    }
}
