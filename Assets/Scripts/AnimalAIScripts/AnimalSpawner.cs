using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {
    enum ThreatLevel {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public GameObject animalPrefab;
    public Transform spawnPoint;
    private float Health;
    private ThreatLevel threatLevel;

    private void Start() {
        for (int i = 0; i < 2; i++) {
            SpawnAnimal(i);
        }
    }

    private void SpawnAnimal(int animalID) {

        GameObject newanimal = Instantiate(animalPrefab, spawnPoint.position, Quaternion.identity);
        newanimal.name = "Animal";

        threatLevel = GetThreatLevel();
        setStats(threatLevel);

        
        AnimalController animalController = newanimal.GetComponent<AnimalController>();
        if (animalController != null)
        {
            animalController.setAnimalHealth(Health);
            animalController.setMaxAnimalHealth(Health);
            animalController.setanimalID(animalID);
            animalController.setGameObject(newanimal);
            animalController.setThreatLevel((int)threatLevel);
        }
    }

    private ThreatLevel GetThreatLevel() {
        
        int ranNum = UnityEngine.Random.Range(1, 4);

        ThreatLevel threatLevel = (ThreatLevel)ranNum;
        return threatLevel;
    }

    private void setStats(ThreatLevel threatLevel) {
        
        switch (threatLevel) {
            case ThreatLevel.Low:
                Health = 15;
                break;

            case ThreatLevel.Medium:
                Health = 25;
                break;
                
            case ThreatLevel.High:
                Health = 35;
                break;
        }
    }
}
