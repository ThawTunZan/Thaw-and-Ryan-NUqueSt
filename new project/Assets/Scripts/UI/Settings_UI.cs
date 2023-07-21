using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_UI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlPanel;
    public GameObject creditsPanel;
    public GameObject volumePanel;
    public GameObject optionPanel;

    private TextMeshProUGUI controlPanelText;
    private TextMeshProUGUI creditsText;

    private PlayerMovement playerMovement;
    private PlayerItems playerItems;

    private bool settingsActive;

    public AudioSource audioSource;
    public Slider slider;

    private void Start()
    {
        audioSource = GameObject.Find("BGMManager").GetComponent<AudioSource>();
        controlPanelText = controlPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        controlPanelText.text = "W A S D - Movement\nESC - Options Panel/Close Active UI\nTAB - Inventory Panel\nQ - Quest Panel\nE - Interact" +
            "\nSpace Bar - Proceed Dialogue\nLeft Click - Use Item in Toolbar\nRight Click - Use Item in Toolbar";

        creditsText = creditsPanel.transform.Find("ControlsHeader").transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        creditsText.text = "Game Developers: Thaw Tun Zan, Lee Yan Le Ryan\n\nProject Advisor: Eugene Tang Kang Jie\n\n" +
            "Game Testers: Edwin Zheng Yuan Yi, Toh Li Yuan, Brannon Aw Xu Wei, Sean William Bulawan Villamin, Project Sage" +
            "\n\nPeer Evaluators: Unmei no Farfalla, PestControl, GrassToucher";

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
        controlPanel.SetActive(false);
        creditsPanel.SetActive(false);
        volumePanel.SetActive(false);
        optionPanel.SetActive(false);
        playerItems.disableToolbar = false;
        settingsActive = false;
        playerMovement.enabled = true;
    }

    public void Options()
    {
        optionPanel.SetActive(true);
    }
    
    public void Volume()
    {
        volumePanel.SetActive(true);
    }

    public void Controls()
    {
        controlPanel.SetActive(true);
    }

    public void ToggleOptionsOff()
    {
        controlPanel.SetActive(false);
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
    public void ChangeVolume()
    {
        audioSource.volume = slider.value;
    }
}
