using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ClockManager : MonoBehaviour, IDataPersistence
{
    public static ClockManager instance;

    public float hours;
    public float minutes;
    public float days;
    public float seconds;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;
    [SerializeField] private Volume ppv;

    public PlayerPositionSO startingPosition;

    private float tick;

    //public Animator torch;
    private List<Animator> animatorTorchList;
    GameObject[] taggedObjects;

    private void Awake()
    {
    }
    void Start()
    {
        dayText = GameObject.Find("Day").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        if (startingPosition.transittedScene) {
            hours = GameManager.instance.hours;
            minutes = GameManager.instance.minutes;
            days = GameManager.instance.day;
            seconds = GameManager.instance.seconds;

        }
        else if (startingPosition.playerDead)
        {
            hours = 8;
            minutes = 0;
            seconds = 0;
            days = GameManager.instance.day;
        }
        else
        {
            hours = 8;
            minutes = 0;
        }

        animatorTorchList = new List<Animator>();
        FindAndAddAnimators();
    }

    private void FindAndAddAnimators()
    {
        taggedObjects = GameObject.FindGameObjectsWithTag("torch");

        foreach (GameObject obj in taggedObjects)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animatorTorchList.Add(animator);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.hours = hours;
        GameManager.instance.minutes = minutes;
        GameManager.instance.seconds = seconds;
        GameManager.instance.day = days;
        string bufferMinutes = "";
        string bufferHours = "";
        if (hours < 10)
        {
            bufferHours = "0";
        }
        if (minutes < 10)
        {
            bufferMinutes = "0";
        }
        dayText.text = "Day: " + days;
        timeText.text = "Time: " + bufferHours +hours + " " + bufferMinutes + minutes;
        CalcTime();
    }

    public void CalcTime()
    {
        tick += Time.fixedDeltaTime;

        if (tick >= 1)
        {
            tick = 0;
            seconds += 1;
        }

        if (seconds >= 64)
        {
            minutes += 15;
            seconds = 0;
        }
        if (minutes >= 60)
        {
            hours += 1;
            minutes = 0;
        }
        if (hours > 23)
        {
        }
        ControlPPV();
    }

    public void ControlPPV()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        bool isInside = (sceneName == "FarmHouse" || sceneName == "Game" || sceneName == "Village");
        if (hours >= 18 && hours <= 21 && !isInside)
        {
            ppv.weight = (((hours - 18) * 60) + minutes) / 240;
            foreach (Animator animator in animatorTorchList)
            {
                animator.SetBool("isNoon", true);
            }
            foreach (GameObject obj in taggedObjects)
            {
                Light2D lightComponent = obj.GetComponent<Light2D>();
                lightComponent.intensity = ppv.weight;
            }
        }
        else if (hours >= 8 && hours < 18)
        {
            ppv.weight = 0;
            foreach (Animator animator in animatorTorchList)
            {
                animator.SetBool("isNoon", false);
            }
            foreach (GameObject obj in taggedObjects)
            {
                Light2D lightComponent = obj.GetComponent<Light2D>();
                lightComponent.intensity = 0;
            }
        }
        else if (isInside)
        {
            ppv.weight = 0;
        }
    }
    public void LoadData(GameData data)
    {
        days = data.day;
        hours = data.hours;
    }
    public void SaveData(GameData data)
    {
        data.day = days;
    }
}
