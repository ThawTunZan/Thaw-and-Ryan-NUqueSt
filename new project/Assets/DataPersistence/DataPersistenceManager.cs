using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storange Config")]
    [SerializeField] private string fileName;

    private FileDataHandler dataHandler;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set; }

    public bool sceneTransitted;



    //to maintain singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.-Thaw");
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        sceneTransitted = false;
    }

    //calls OnSceneLoaded function and OnSceneUnloaded respectively for when this gameObject is enabled
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        
    }

    //calls the findAllDataPersistenceObjects function followed by the LoadGame
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        if (!sceneTransitted)
        {
            LoadGame();
        }
    }

    //Saves the game when the scene is unloaded
    public void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnloaded called");
        SaveGame();
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    /*
     *Current gameData is equal to the data that is being loaded when being called by the Load() function that is located in the fileDataHandler class/script
     *Iterates through the list of all the gameObjects that implements the interface IDataPersistence and calls the LoadData method in each of their respective gameObjects
    */
    public async void LoadGame()
    {
        this.gameData = await DatabaseManager.instance.LoadGameData();
       // this.gameData = dataHandler.Load();
        //if no saved data found
        if (this.gameData == null)
        {       
            Debug.Log("No saved data was found. New game needs to be started. Please click NewGame Button -Thaw");

            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    /*
     * Same as LoadGame() just that it is saving instead and there is an addition line of code that calls the method Save(gameData) in the FileDataHandler script/class
     */
    public void SaveGame()
    {
        //reference to PlayerMovement's method
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }
        //saves in local folder
        dataHandler.Save(gameData);
        DatabaseManager.instance.CreateUser(gameData);
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    
    public bool HasGameData()
    {
        //LoadGame();
        return this.gameData != null;
    }
    
}
