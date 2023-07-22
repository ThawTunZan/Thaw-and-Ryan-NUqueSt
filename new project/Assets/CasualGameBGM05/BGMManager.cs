using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    public static BGMManager instance;

    private bool isPlaying0 = false;
    private bool isPlaying1 = false;
    private bool isPlaying2 = false;
    private bool isPlaying3 = false;
    private bool isPlaying4 = false;

    private float tempVolume;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        bool isInside = (sceneName == "GeneralShop" || sceneName == "PlayerHouse" || sceneName == "Village_WeaponShop"
            || sceneName == "GeologistHouse" || sceneName == "ScientistHouse" || sceneName == "TownMayorHouse"
            || sceneName == "NerdNPC House" || sceneName == "TownCentre" || sceneName == "ArtistHouse" || sceneName == "BusinessHouse");
        bool inCave = (sceneName == "Cave_1" || sceneName == "Cave_1a" || sceneName == "Cave_2a" || sceneName == "Cave_3a"
            || sceneName == "Cave_4a" || sceneName == "Cave_5a" || sceneName == "Cave_1b" || sceneName == "DCave_1" 
            || sceneName == "DCave_1a" || sceneName == "DCave_2a");
        if (sceneName == "SampleScene 1")
        {
            audioSource.clip = audioClips[0];
            if (!isPlaying0)
            {
                audioSource.Play();
                isPlaying0 = true;
                isPlaying1 = false;
                isPlaying2 = false;
                isPlaying3 = false;
                isPlaying4 = false;
            }
        }
        else if (sceneName == "Arena")
        {
            PlayerQuests playerQuests = GameObject.Find("Player").GetComponent<PlayerQuests>();
            if (playerQuests.endingProgress == 0)
            {
                audioSource.clip = audioClips[4];
                if (!isPlaying4)
                {
                    audioSource.Play();
                    isPlaying0 = false;
                    isPlaying1 = false;
                    isPlaying2 = false;
                    isPlaying3 = false;
                    isPlaying4 = true;
                }
            }
            else
            {
                audioSource.clip = audioClips[3];
                if (!isPlaying1)
                {
                    audioSource.Play();
                    isPlaying0 = false;
                    isPlaying1 = true;
                    isPlaying2 = false;
                    isPlaying3 = false;
                    isPlaying4 = false;
                }
            }
        }
        else if (!isInside && !inCave)
        {
            audioSource.clip = audioClips[3];
            if (!isPlaying1)
            {
                audioSource.Play();
                isPlaying0 = false;
                isPlaying1 = true;
                isPlaying2 = false;
                isPlaying3 = false;
                isPlaying4 = false;
            }
        }
        else if (inCave)
        {
            audioSource.clip = audioClips[2];
            if (!isPlaying2)
            {
                audioSource.Play();
                isPlaying0 = false;
                isPlaying1 = false;
                isPlaying2 = true;
                isPlaying3 = false;
                isPlaying4 = false;
            }
        }
        else if (isInside)
        {
            audioSource.clip = audioClips[1];
            if (!isPlaying3)
            {
                audioSource.Play();
                isPlaying0 = false;
                isPlaying1 = false;
                isPlaying2 = false;
                isPlaying3 = true;
                isPlaying4 = false;
            }
        }
    } 
}
