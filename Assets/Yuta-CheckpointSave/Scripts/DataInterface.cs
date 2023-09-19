using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataInterface 
{
    void LoadData(GameData gameData); //Only needs to read the data.
    void SaveData(ref GameData gameData); //The pass by reference is to allow modification the data
}
