using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    private void Awake()
    {
    }
    void Start()
    {
        if (startingPosition.transittedScene) {
            hours = GameManager.instance.hours;
            minutes = GameManager.instance.minutes;
            days = GameManager.instance.day;
            // ppv = gameObject.GetComponent<Volume>();
        }
        else
        {
            hours = 8;
            minutes = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.hours = hours;
        GameManager.instance.minutes = minutes;
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
            //hours = 8;
            //days += 1;
        }
        ControlPPV();
    }

    public void ControlPPV()
    {
        if (hours >= 18 && hours <= 21)
        {
            //print((((hours - 18) * 60 + minutes) / 240));
            ppv.weight = (((hours - 18) * 60) + minutes) / 240;
        }
        else if (hours >= 8 && hours < 18)
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
