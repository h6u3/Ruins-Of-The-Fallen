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
            stone.minedCount++;
            stone.SpawnSmallerCube(transform.position); // Spawn the smaller cube
        }
    }
}
