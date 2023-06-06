using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ClockManager : MonoBehaviour
{
    public static ClockManager instance;

    public int hours;
    public int minutes;
    public int days;
    public float seconds;
    //public GameObject dayGameObj;
    public TextMeshProUGUI dayText;
    //public GameObject timeGameObj;
    public TextMeshProUGUI timeText;
    public Volume ppv;

    private float tick;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one ClockManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
        hours = 20;
        minutes = 0;
        days = 0;
        tick = 0.1f;
        ppv = gameObject.GetComponent<Volume>();


    }

    // Update is called once per frame
    void Update()
    {
        dayText.text = "Day: " + days;
        timeText.text = "Time: " + hours + " " + minutes;
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
            hours = 8;
            days += 1;
        }
        ControlPPV();
    }

    public void ControlPPV()
    {
        if (hours >= 18 && hours <= 21)
        {
            ppv.weight = ((hours - 18) + minutes) / 240;
        }
    }
}
