using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileManager(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        //Accounting for different OS's using different path seperators. Makes it easier and safer to use Combine 
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullpath))
        {
            try
            {
                string dataToLoad = "";

                //Read the serialized data from the Json file into a string
                using (FileStream fs = new FileStream(fullpath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                //Deserialize the data from Json into the GameData
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error while trying to load from file: " + fullpath + "\n" + e.Message);

            }
        }
        return loadedData;
    }

    public void save(GameData data)
    {
        //Accounting for different OS's using different path seperators. Makes it easier and safer to use Combine 
        string fullpath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //Create the directory in case it is not already made.
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));

            //Serialize a GameData obj to a Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //Write the file to the system
            using (FileStream fs = new FileStream(fullpath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //Write to the file
                    sw.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while trying to save to file: " + fullpath + "\n" + e.Message);
        }
    }
}
