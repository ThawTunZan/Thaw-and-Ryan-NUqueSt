using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Settings_UI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    private GameObject player;
    private PlayerMovement playerMovement;
    float originalSpeed;

    private FreezePlayerMovement freezeMovement;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ToggleSettings();
            ToggleOptionsOff();
            ToggleCreditsOff();
        }
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            if (!settingsPanel.activeSelf)
            {
                if (playerMovement.movespeed != 0) 
                {
                    originalSpeed = playerMovement.movespeed;
                }
                ToggleMovementOff();
                settingsPanel.SetActive(true);
            }
            else
            {
                ToggleMovementOn();
                settingsPanel.SetActive(false);
            }
        }
    }

    public void ToggleSettingsOff()
    {
        if (settingsPanel != null)
        {
            if (!settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(true);
            }
            else
            {
                settingsPanel.SetActive(false);
            }
        }
    }

    public void ToggleOptions()
    {
        if (optionsPanel != null)
        {
            if (!optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(true);
            }
            else
            {
                optionsPanel.SetActive(false);
            }
        }
    }

    public void ToggleOptionsOff()
    {
        if (optionsPanel != null)
        {
            if (optionsPanel.activeSelf)
            {
                ToggleMovementOn();
                ToggleOptions();
            }
            if (settingsPanel.activeSelf)
            {
                ToggleMovementOff();
            }
        }
    }

    public void ToggleCredits()
    {
        if (creditsPanel != null)
        {
            if (!creditsPanel.activeSelf)
            {
                creditsPanel.SetActive(true);
            }
            else
            {
                creditsPanel.SetActive(false);
            }
        }
    }

    public void ToggleCreditsOff()
    {
        if (creditsPanel != null)
        {
            if (creditsPanel.activeSelf)
            {
                ToggleMovementOn();
                ToggleCredits();
            }
            if (settingsPanel.activeSelf)
            {
                ToggleMovementOff();
            }
        }
    }

    public void ToggleMovementOn()
    {
        playerMovement.movespeed = originalSpeed;
    }

    public void ToggleMovementOff()
    {
        playerMovement.movespeed = 0;
    }

    public void ReturnToGame()
    {
        ToggleSettingsOff();
        ToggleOptionsOff();
        ToggleCreditsOff();
        ToggleMovementOn();
    }

    public void Options()
    {
        ToggleOptions();
        ToggleSettingsOff();
    }

    public void Credits()
    {
        ToggleCredits();
        ToggleSettingsOff();
    }

    public void ExitToMainMenu()
    {

    }
}
