using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using PlayFab.ClientModels;
using PlayFab;

public class DatabaseManager : MonoBehaviour
{
    public string userID;
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
    
    public void CreateUser(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {userID, json}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    
    public GameData LoadGameData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnCharactersDataRecieved, OnLoadGameDataError);
        return databaseGameData;
    }

    public void OnDataSend(UpdateUserDataResult result)
    {
      //  Debug.Log("Successfully saved!");
    }
    public void OnCharactersDataRecieved(GetUserDataResult result)
    {
      //  Debug.Log("Recieved character data!");
        if (result.Data != null && result.Data.ContainsKey(userID))
        {
           // print(result.Data[userID].Value);
            databaseGameData = JsonUtility.FromJson<GameData>(result.Data[userID].Value);

        }
        else
        {
            Debug.LogWarning("User data not found - at DatabaseManager.cs");
        }
    }
    void OnError(PlayFabError error)
    {
        print(error.ErrorMessage);
    }

    void OnLoadGameDataError(PlayFabError error)
    {
        Debug.LogError("Error loading game data: " + error.ErrorMessage);
    }

}