using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using PlayFab;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    public static DatabaseManager instance { get; private set; }
    public GameData databaseGameData;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one DatabaseManager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void CreateUser(GameData data, string userName)
    {
        string json = JsonUtility.ToJson(data);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {userName, json}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    
    public GameData LoadGameData(string userName)
    {
        userID = userName;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataRecieved, OnError);
        return databaseGameData;
    }

    public void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successfully saved!");
    }
    public void OnCharactersDataRecieved(GetUserDataResult result)
    {
        Debug.Log("Recieved character data!");
        if (result.Data != null && result.Data.ContainsKey(userID))
        {
            databaseGameData = JsonUtility.FromJson<GameData>(result.Data[userID].Value);
            
        }
        Debug.LogWarning("User data not found - at DatabaseManager.cs");
    }
    void OnError(PlayFabError error)
    {
        print(error.ErrorMessage);
    }

}