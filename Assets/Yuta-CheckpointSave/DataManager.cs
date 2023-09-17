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
    private bool playerInside = false; //For detecting the user 

    //Apply a mesh collider to a game object (e.g. plane) with isTrigger enabled.
    //Then apply the DataManager (this) file onto the plane, then it will be used as the save area.

    private void OnTriggerEnter(Collider other)
    {
        //If the collider detectes a player, set true
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Player entered the save area."); //Logs to check functionality
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the collider no longer detectes a player, set false
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Player exited the save area."); //Logs to check functionality
        }
    }

    private void Update()
    {
        if(playerInside) //Allow for saving when the user is detected to be in the save area
        {
            if (Input.GetKeyDown(KeyCode.X)) //Press X to save the game
            {
                SaveGame();
                Debug.Log("Saved!"); //Debug to check if it saved correctly.
            }
        }
    }

    //Commenting out to remove the singleton pattern, and allow for multiple save managers / runes
    //public void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Debug.LogError("Found more than one data managers!");
    //    }
    //    instance = this;
    //}

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
