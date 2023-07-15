using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDetectionCG1111 : MonoBehaviour
{
    public CompleteCg1111A questComplete;
    public string colorName;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("robot"))
        {
            questComplete.colorsDetected.Add(colorName);
        }
    }
}
