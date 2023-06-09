using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreezePlayerMovement : MonoBehaviour
{
    public List<GameObject> listOfPanels = new List<GameObject>();

    private GameObject player;
    private PlayerMovement playerMovement;
    public float originalSpeed;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public void CheckForUI()
    {
        bool hasActivePanel = listOfPanels.Any(panel => panel.activeSelf);

        if (hasActivePanel)
        {
            playerMovement.movespeed = 0;
        }
        else if (originalSpeed != 0)
        {
            playerMovement.movespeed = originalSpeed;
            originalSpeed = 0;
        }
    }
}
