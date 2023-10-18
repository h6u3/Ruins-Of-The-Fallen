using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodHealth : MonoBehaviour
{
    public float health;
    [SerializeField] private WoodManager wood;
    private bool hasDestroyed = false;

    void Start()
    {
        wood = FindObjectOfType<WoodManager>();
    }

    void FixedUpdate()
    {
        if (health <= 0 && !hasDestroyed)
        {
            hasDestroyed = true;
            Destroy(gameObject);

            Rigidbody rb = wood.treePrefab.GetComponent<Rigidbody>();
            rb.AddForce(wood.treePrefab.transform.forward * 4);
            wood.SpawnTree(transform.position);
            wood.SpawnLogs();
        }
    }
}
