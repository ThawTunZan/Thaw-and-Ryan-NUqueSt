using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Settings_UI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    private TextMeshProUGUI optionsText;
    private TextMeshProUGUI creditsText;

    private FreezePlayerMovement freezePlayerMovement;
    private PlayerItems playerItems;

    private void Start()
    {
        optionsText = optionsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        optionsText.text = "W A S D - Movement\nESC - Options Panel\nTAB - Inventory Panel\nQ - Quest Panel\nE - Interact" +
            "\nSpace Bar - Proceed Dialogue\nLeft Click - Use Item in Toolbar\n";

        creditsText = creditsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        creditsText.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei";

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
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
