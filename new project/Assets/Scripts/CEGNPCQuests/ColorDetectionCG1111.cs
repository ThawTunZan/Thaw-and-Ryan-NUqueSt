using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetectionCG1111 : MonoBehaviour
{
    public CompleteCEGQuest questComplete;
    public string colorName;
    public bool hasVisited;
    // Start is called before the first frame update
    void Start()
    {
        hasVisited = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot")  && !hasVisited)
        {
            questComplete.colorsDetected.Add(colorName);
            hasVisited = true;
        }
    }

    private bool SameColor(string color)
    {
        foreach (string indexColor in questComplete.colorsDetected)
        {
            if (color == indexColor)
            {
                return true;
            }
        }
        return false;
    }
}
