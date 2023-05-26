using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;

public class DatabaseManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;
    public static DatabaseManager instance { get; private set; }
    public string userEmail;
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
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser(GameData data, string userName)
    {
        string json = JsonUtility.ToJson(data);
        dbReference.Child("users").Child(userName).SetRawJsonValueAsync(json);
    }


    public async Task<GameData> LoadGameData(string userName)
    {
        if (!string.IsNullOrEmpty(userName))
        {
          var task = dbReference.Child("users").Child(userName).GetValueAsync();

             await task; // Wait for the task to complete asynchronously

            if (task.IsFaulted)

            {
                Debug.LogError("Failed to retrieve user data: " + task.Exception);
                return null;
            }

            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                string jsonData = snapshot.GetRawJsonValue();
                GameData gameData = JsonUtility.FromJson<GameData>(jsonData);
                PlayerItems playerItems = FindObjectOfType<PlayerItems>();
                return gameData;
            }
            else
            {
                Debug.LogWarning("User data not found!");
                return null;
            }
        }
        else
        {
            Debug.LogWarning("User ID is empty!");
            return null;
        }
    }
}