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
    private DatabaseReference userRef;
    public static DatabaseManager instance { get; private set; }
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
        userID = SystemInfo.deviceUniqueIdentifier;
        //Get the root reference location of database
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        userRef = dbReference.Child("users").Child(userID);
    }

    public void CreateUser(GameData data)
    {
        string json = JsonUtility.ToJson(data);
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }


    public async Task<GameData> LoadGameData()
    {
        if (!string.IsNullOrEmpty(userID))
        {
            var task = userRef.GetValueAsync();

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

    // Other code...
}