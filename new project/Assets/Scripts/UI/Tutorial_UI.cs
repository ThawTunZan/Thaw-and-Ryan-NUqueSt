using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial_UI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public TextMeshProUGUI panelText;

    private bool movementDone;
    public bool pressedW;
    public bool pressedA;
    public bool pressedS;
    public bool pressedD;

    private bool pickSwordDone;
    public GameObject pickSwordFirst;
    public PlayerItems playerItems;

    private bool openInventoryDone;
    public PlayerMovement playerMovement;
    public GameObject removeItemPanel;
    private float originalSpeed;

    private bool moveSwordDone;

    private bool closeInventoryDone;
    public SlimeAI slimeMovement;

    private bool attackSwordDone;
    public GameObject attackSwordFirst;

    private bool killSlimeDone;

    public bool runToPortal;

    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = playerMovement.movespeed;
        slimeMovement.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementDone)
        {
            MovementCheck();
        }
        else if (!pickSwordDone)
        {
            PickSwordCheck();
        }
        else if (!openInventoryDone)
        {
            OpenInventoryCheck();
        }
        else if (!moveSwordDone)
        {
            MoveSwordCheck();
        }
        else if (!closeInventoryDone)
        {
            CloseInventoryCheck();
        }
        else if (!attackSwordDone)
        {
            AttackSwordCheck();
        }
        else if (!killSlimeDone)
        {
            KillSlimeCheck();
        }
        else if (runToPortal)
        {
            RunToPortal();
        }
    }

    void MovementCheck()
    {
        if (!pressedW && Input.GetKeyDown(KeyCode.W))
        {
            pressedW = true;
        }
        else if (!pressedA && Input.GetKeyDown(KeyCode.A))
        {
            pressedA = true;
        }
        else if (!pressedS && Input.GetKeyDown(KeyCode.S))
        {
            pressedS = true;
        }
        else if (!pressedD && Input.GetKeyDown(KeyCode.D))
        {
            pressedD = true;
        }
        if (pressedW && pressedA && pressedS & pressedD)
        {
            movementDone = true;
            panelText.text = "Walk over to the sword to pick it up";
        }
    }

    void PickSwordCheck()
    {
        if (!playerItems.inventory.slots[0].IsEmpty)
        {
            Destroy(pickSwordFirst);
            playerMovement.movespeed = 0;
            pickSwordDone = true;
            panelText.text = "Press TAB to open up your inventory";
        }
    }

    void OpenInventoryCheck()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            removeItemPanel.SetActive(false);
            openInventoryDone = true;
            panelText.text = "Hold left click and drag the sword into your toolbar by releasing left click";
        }
    }

    void MoveSwordCheck()
    {
        for (int i = 0; i < 7; i++)
        {
            if (!playerItems.toolbar.slots[i].IsEmpty)
            {
                moveSwordDone = true;
                panelText.text = "Press TAB to close inventory";
            }
        }
    }

    void CloseInventoryCheck()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            closeInventoryDone = true;
            panelText.text = "Attack by pressing LEFT CLICK on your mouse";
        }
    }

    void AttackSwordCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            removeItemPanel.SetActive(true);
            playerMovement.movespeed = originalSpeed;
            attackSwordDone = true;
            slimeMovement.enabled = true;
            panelText.text = "Get rid of the slime blocking the way";
        }
    }

    void KillSlimeCheck()
    {
        if (slimeMovement == null)
        {
            killSlimeDone = true;
            panelText.text = "Continue running to the right!";
        }
    }

    void RunToPortal()
    {
        panelText.text = "There is no other choice. Get in the portal!";
    }
}
