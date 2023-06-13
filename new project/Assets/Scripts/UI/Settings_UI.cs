using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Settings_UI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    private FreezePlayerMovement freezePlayerMovement;
    private PlayerItems playerItems;

    private void Start()
    {
        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
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
                settingsPanel.SetActive(true);
                playerItems.disableToolbar = true;
                freezePlayerMovement.ToggleMovement();
            }
            else
            {
                settingsPanel.SetActive(false);
                playerItems.disableToolbar = false;
                freezePlayerMovement.ToggleMovement();
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
                ToggleOptions();
                playerItems.disableToolbar = false;
                freezePlayerMovement.ToggleMovement();
            }
            if (settingsPanel.activeSelf)
            {
                playerItems.disableToolbar = true;
                freezePlayerMovement.ToggleMovement();
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
                ToggleCredits();
                playerItems.disableToolbar = false;
                freezePlayerMovement.ToggleMovement();
            }
            if (settingsPanel.activeSelf)
            {
                playerItems.disableToolbar = true;
                freezePlayerMovement.ToggleMovement();
            }
        }
    }

    public void ReturnToGame()
    {
        ToggleSettingsOff();
        ToggleOptionsOff();
        ToggleCreditsOff();
        freezePlayerMovement.ToggleMovement();
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
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}
