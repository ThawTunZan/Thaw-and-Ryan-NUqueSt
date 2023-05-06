using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mmPanel;
    public AudioMixer volumeMixer;

  
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void disableMainMenu()
    {
        mmPanel.SetActive(false);
    }
    public void enableMainMenu()
    {
        mmPanel.SetActive(true);
    }
    public void setVolume(float MasterVolume)
    {
        volumeMixer.SetFloat("MasterVolume", MasterVolume);
    }


  
    public void ExitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
