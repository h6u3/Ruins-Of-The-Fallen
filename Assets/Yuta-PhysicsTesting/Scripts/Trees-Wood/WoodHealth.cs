using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private WoodManager wood;

    void Start()
    {
        wood = FindObjectOfType<WoodManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            Vector3 spawnPosition = new Vector3(transform.position.x, 1f, transform.position.z); //z
            wood.SpawnLog(spawnPosition); // Spawn a smaller log with random deviation
        }
    }
}
