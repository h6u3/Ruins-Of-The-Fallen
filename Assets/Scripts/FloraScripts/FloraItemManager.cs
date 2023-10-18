using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraItemManager : MonoBehaviour
{
    public float health;
   [SerializeField] private FloraManager flora;
   [SerializeField] private GameObject Item;
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
                spawnFloraItems(spawnPosition);
            }
            flora.harvested = true;
        }
    }

    public void spawnFloraItems(Vector3 position)
    {
        position += new Vector3(0,1,0);
        Instantiate(Item, position, Quaternion.identity);
    }
}
