using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuScreen : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject loginMenu;

    public void LoginButtonPressed()
    {
        startMenu.SetActive(false);
    }

    public void RegisterButtonPressed()
    {
        startMenu.SetActive(false);
        loginMenu.SetActive(false);
    }
}
