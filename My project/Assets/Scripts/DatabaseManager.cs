using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using UnityEngine.UI;


public class DatabaseManager : MonoBehaviour
{
    InputField Email;
    DatabaseReference reference;
    InputField showLoadedText;
    // Start is called before the first frame update
    void Start()
    {
        //Set up the editor before calling into the realtime database
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://orbital-55a95-default-rtdb.asia-southeast1.firebasedatabase.app/");

        //Get the root reference location of the database
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Update()
    {
        
    }

    public void saveData()
    {
        reference.Child("Users").Child("User 1").Child("Email").SetValueAsync(name);

    }
    public void loadData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Leaders")
        .ValueChanged += DatabaseManager_ValueChanged;
    }

    private void DatabaseManager_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        showLoadedText.text = e.Snapshot.Child("User 1").Child("Email").GetValue(true).ToString();
    }
}
