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

    /*
     * Checks if any of the lists in listOfPanels is active. If active, then bool hasActivePanel becomes true. If it's true, then
     * player's movespeed is set to 0. If no lists inside the listOfPanels are active, then bool hasActivePanel is false, and if 
     * originalSpeed of the player is not zero, then the player's movespeed is set to the originalSpeed. originalSpeed is also set
     * to 0.
     */
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

    public void ToggleMovement()
    {
        if (playerMovement.movespeed != 0)
        {
            originalSpeed = playerMovement.movespeed;
        }
        CheckForUI();
    }
}
