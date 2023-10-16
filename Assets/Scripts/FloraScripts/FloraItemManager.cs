using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraItemManager : MonoBehaviour
{
    public float health;
   [SerializeField] private FloraManager flora;
    void Start()
    {
        flora = FindObjectOfType<FloraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !flora.harvested)
        {
            int FloraToSpawn = Random.Range(1, 3);

            for (int i = 0; i < FloraToSpawn; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f),
                                                                         Random.Range(0.01f, 0.5f),
                                                                         Random.Range(-0.5f, 0.5f));
                flora.SpawnFloraItems(spawnPosition);
            }
            flora.harvested = true;
        }
    }
}
