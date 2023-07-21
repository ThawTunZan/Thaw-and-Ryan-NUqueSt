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

    public TextMeshProUGUI goToSleepText;

    public PlayerPositionSO startingPosition;

    private float tick;

    //public Animator torch;
    private List<Animator> animatorTorchList;
    GameObject[] taggedObjects;

    private PlayerTutorial playerTutorial;

    private void Awake()
    {
    }

    void Start()
    {
        dayText = GameObject.Find("Day").GetComponent<TextMeshProUGUI>();
        timeText = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        goToSleepText = GameObject.Find("GoToSleepText").GetComponent<TextMeshProUGUI>();
        playerTutorial = GameObject.Find("Player").GetComponent<PlayerTutorial>();
        
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
        if (playerTutorial.tutorialProgress >= 3)
        {
            CalcTime();
        }
        else if (playerTutorial.tutorialProgress == 2)
        {
            hours = 8;
            ControlPPV();
            dayText.text = "";
            timeText.text = "";
            goToSleepText.text = "";
        }
        else
        {
            hours = 22;
            ControlPPV();
            dayText.text = "";
            timeText.text = "";
            goToSleepText.text = "";
        }
    }

    public void CalcTime()
    {
        tick += Time.fixedDeltaTime;

        if (tick >= 0.4)
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
        bool isInside = (sceneName == "GeneralShop" || sceneName == "PlayerHouse" || sceneName == "Village_WeaponShop"
            || sceneName == "GeologistHouse" || sceneName == "ScientistHouse" || sceneName == "TownMayorHouse"
            || sceneName == "NerdNPC House" || sceneName == "TownCentre" || sceneName == "ArtistHouse" || sceneName == "BusinessHouse");
        bool inCave = (sceneName == "Cave_1" || sceneName == "Cave_1a" || sceneName == "Cave_2a" || sceneName == "Cave_3a" 
            || sceneName == "Cave_4a" || sceneName == "Cave_1b" || sceneName == "DCave_1" || sceneName == "DCave_1a" 
            || sceneName == "DCave_2a");
        timeText.color = Color.white;
        goToSleepText.text = "";
        if (hours >= 18 && hours <= 21 && !isInside && !inCave)
        {
            ppv.weight = (((hours - 18) * 60) + minutes) / (float)(240 / 0.7);
            ChangeTorchIntensity(ppv.weight);
        }
        else if (hours >= 8 && hours < 18 && !isInside && !inCave)
        {
            ppv.weight = 0;
            SFXManager.instance.hasPlayedWarning = false;
        }
        else if (hours >= 22)
        {
            timeText.color = Color.red;
            goToSleepText.text = "GO BACK HOME TO SLEEP!!!";
            if (!SFXManager.instance.hasPlayedWarning && playerTutorial.tutorialProgress >= 3)
            {
                SFXManager.instance.audioSource.clip = SFXManager.instance.audioClips[3];
                SFXManager.instance.audioSource.Play();
                SFXManager.instance.hasPlayedWarning = true;
            }
            if (!isInside)
            {
                ppv.weight = 0.7f;
                ChangeTorchIntensity(1);
            }
        }
        else if (isInside)
        {
            ppv.weight = 0f;
        }
        else if (inCave)
        {
            ppv.weight = 0.7f;
            ChangeTorchIntensity(1);
        }
    }

    private void ChangeTorchIntensity(float intensity)
    {
        foreach (Animator animator in animatorTorchList)
        {
            animator.SetBool("isNoon", true);
        }
        foreach (GameObject obj in taggedObjects)
        {
            Light2D lightComponent = obj.GetComponent<Light2D>();
            lightComponent.intensity = intensity;
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
