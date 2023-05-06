/**
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using TMPro;
using Firebase.Database;
using Firebase.Auth;

public class PlayerData : MonoBehaviour
{
   // private string playerEmail = FireBaseAuth.playeremail;
    private DatabaseReference databaseReference;

    // Use this for initialization
    FirebaseAuth auth;
    FirebaseUser user;
    DatabaseReference databaseRef;
    void Start()
    {
        // Initialize Firebase SDK
       // FirebaseApp.DefaultInstance.SetDatabaseUrl("https://orbital-55a95-default-rtdb.asia-southeast1.firebasedatabase.app/");

        // Get a reference to the database
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });

    }


    
    public string datapath = "C:/Users/Thaw Tun Zan/AppData/LocalLow/ThawHai/Orbital";

    public void UploadPlayerDataFromFile(string playerName, string dataPath)
    {
        // Read the JSON file from disk
        string playerDataJson = File.ReadAllText(dataPath);

        // Create a new child node for the player data under the "players" node
        DatabaseReference playerNode = databaseReference.Child("players").Child(playerName);

        // Convert the JSON string to a dictionary
        Dictionary<string, object> playerDataDict = JsonUtility.FromJson<Dictionary<string, object>>(playerDataJson);

        // Set the player data in the database
        playerNode.SetValueAsync(playerDataDict).ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to upload player data: " + task.Exception.ToString());
                return;
            }
            else
            {
                Debug.Log("Player data uploaded successfully.");
            }
        });
    }

    
}
*/
