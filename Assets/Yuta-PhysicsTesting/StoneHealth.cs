using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHealth : MonoBehaviour
{
    public float health;
    private StoneManager stone;
    void Start()
    {
        stone = FindObjectOfType<StoneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            int numberOfCubesToSpawn = Random.Range(1, 3); // Random number between 1 and 5 (inclusive)

            for (int i = 0; i < numberOfCubesToSpawn; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f),  //x
                                                                         Random.Range(0.01f, 0.5f),  //y
                                                                         Random.Range(-0.5f, 0.5f)); //z
                stone.SpawnSmallerCube(spawnPosition); // Spawn a smaller cube with random deviation
            }
        }
    }
}
