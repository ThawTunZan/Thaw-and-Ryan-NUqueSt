using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private bool settingsActive;

    private void Start()
    {
        optionsText = optionsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        optionsText.text = "W A S D - Movement\nESC - Options Panel/Close Active UI\nTAB - Inventory Panel\nQ - Quest Panel\nE - Interact" +
            "\nSpace Bar - Proceed Dialogue\nLeft Click - Use Item in Toolbar\n";

        creditsText = creditsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        creditsText.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei";

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
    }

    void Update()
    {
        if (!playerItems.disableToolbar && Input.GetKeyDown(KeyCode.Escape)) 
        {
            settingsPanel.SetActive(true);
            playerItems.disableToolbar = true;
            settingsActive = true;
            playerMovement.enabled = false;
        }
        else if (playerItems.disableToolbar && settingsActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToGame();
        }
    }

    public void ReturnToGame()
    {
        settingsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        playerItems.disableToolbar = false;
        settingsActive = false;
        playerMovement.enabled = true;
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void ToggleOptionsOff()
    {
        optionsPanel.SetActive(false);
    }
   
    public void Credits()
    {
        creditsPanel.SetActive(true);
    }

    public void ToggleCreditsOff()
    {
        creditsPanel.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
