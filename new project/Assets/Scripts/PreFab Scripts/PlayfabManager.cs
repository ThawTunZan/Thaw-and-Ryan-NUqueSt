using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayfabManager : MonoBehaviour
{
    //buttons
    public GameObject loginPanel;
    public GameObject mmPanel;
    
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    [Header("register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    public GameObject uiManager;
    public UIManager uiManagerScript;

    void Awake()
    {
        uiManagerScript = uiManager.GetComponent<UIManager>();
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginField.text,
            Password = passwordLoginField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        DataPersistenceManager.instance.userName = passwordLoginField.text;
    }
    //Function for the register button
    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailRegisterField.text,
            Password = passwordRegisterField.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        uiManagerScript.LoginScreen();
        confirmLoginText.text = "Account successfully created! Please Login!";
    }

    void OnLoginSuccess(LoginResult result)
    {
        warningLoginText.text = "";
        confirmLoginText.text = "Logged In";
        LoginScreen();
    }

    void OnError(PlayFabError error)
    {
        warningLoginText.text = error.ErrorMessage;
        warningRegisterText.text = error.ErrorMessage;
        //print(error.ErrorMessage);
        if (passwordLoginField.text.Length < 6)
        {
            warningRegisterText.text = "Password is too short! Please enter a Password of length at least 6 characters!";
            return;
        }
        else if (emailLoginField.text.Length == 0)
        {
            warningRegisterText.text = "Email Register Field is empty! Please enter your email";
            return;
        }
        else if (passwordRegisterField.text.Length == 0)
        {
            warningRegisterText.text = "Password Register Field is empty! Please enter a password!";
            return;
        }
        else
        {
            warningLoginText.text = error.GenerateErrorReport();
            print("error");
            print(error.GenerateErrorReport());
        }
        return;
    }
    public void LoginScreen()
    {
        mmPanel.SetActive(true);
        loginPanel.SetActive(false);
    }
}
