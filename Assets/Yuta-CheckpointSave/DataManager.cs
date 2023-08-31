using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<DataInterface> data;
    private FileManager fileMgr;
    public static DataManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one persistence managers!");
        }
        instance = this;
    }

    private void Start()
    {
        //It will give the OS the standard dir for persisting data in a Unity proj.
        fileMgr = new FileManager(Application.persistentDataPath, fileName);

        //Return all persistence objects and save it into the list
        this.data = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void NewGame()
    {
        this.gameData = new GameData();
    }

    private void LoadGame()
    {
        //Load the file contents 
        this.gameData = fileMgr.Load();

        //Start a new game if the data does not exist
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing a new game and data to default.");
            NewGame();
        }

        //Loop through the persistence data list and load each gamedata
        foreach (DataInterface iData in data)
        {
            iData.LoadData(gameData);
        }
    }

    private void SaveGame()
    {
        foreach (DataInterface iData in data)
        {
            iData.SaveData(ref gameData);
        }
        //Save into the Json file
        fileMgr.save(gameData);
    }

    //TODO: Implement a proper saving system with UI
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<DataInterface> FindAllDataPersistenceObjects()
    {
        //Find all scripts implementing the IDataPersistence interface in the scene
        IEnumerable<DataInterface> data = FindObjectsOfType<MonoBehaviour>()
            .OfType<DataInterface>();

        //Return the returned objects in a List format
        return new List<DataInterface>(data);
    }
}
