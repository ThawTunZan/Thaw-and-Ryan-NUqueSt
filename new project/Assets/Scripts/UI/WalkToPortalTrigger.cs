using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToPortalTrigger : MonoBehaviour
{
    public Tutorial_UI tutorialUI;

    bool playerInRange;

    private void Start()
    {
        tutorialUI = GameObject.Find("Tutorial").GetComponent<Tutorial_UI>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            tutorialUI.runToPortal = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }
}
