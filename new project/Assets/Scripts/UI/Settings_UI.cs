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

    public bool settingsActive;

    private void Start()
    {
        optionsText = optionsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        optionsText.text = "W A S D - Movement\nESC - Options Panel/Close Active UI\nTAB - Inventory Panel\nQ - Quest Panel\nE - Interact" +
            "\nSpace Bar - Proceed Dialogue\nLeft Click - Use Item in Toolbar\n";

        creditsText = creditsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        creditsText.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei";

        freezePlayerMovement = GameObject.Find("Canvas").GetComponent<FreezePlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    void Update()
    {
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Escape)) 
        {
            ToggleSettingsOn();
        }
        else if (playerItems.disableToolbar && settingsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }

    public void ToggleSettingsOn()
    {
        settingsPanel.SetActive(true);
        playerItems.disableToolbar = true;
        settingsActive = true;
        freezePlayerMovement.ToggleMovement();
    }

    public void ToggleSettingsOff()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void ToggleOptionsOn()
    {
        if (!optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(true);
        }
    }

    public void ToggleOptionsOff()
    {
        if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
    }

    public void ToggleCreditsOn()
    {
        if (!creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(true);
        }
    }

    public void ToggleCreditsOff()
    {
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
        }
    }

    public void ReturnToGame()
    {
        ToggleSettingsOff();
        ToggleOptionsOff();
        ToggleCreditsOff();
        playerItems.disableToolbar = false;
        settingsActive = false;
        freezePlayerMovement.ToggleMovement();
    }

    public void Options()
    {
        ToggleOptionsOn();
        ToggleSettingsOff();
    }

    public void Credits()
    {
        ToggleCreditsOn();
        ToggleSettingsOff();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
