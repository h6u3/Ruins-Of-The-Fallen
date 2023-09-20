using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem breakParticlesPrefab;
    [SerializeField] private LayerMask mineableObject;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] public GameObject treePrefab;
    [SerializeField] public GameObject logPrefab;
    private ParticleSystem particles;
    private WoodHealth objectHealth;
    public GameObject fallingTree;

    // Update is called once per frame
    void Update()
    {
        TryBreak();
    }

    public void TryBreak()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, mineableObject))
            {
                objectHealth = hit.transform.GetComponent<WoodHealth>();
                objectHealth.health -= damage * Time.deltaTime; // Decrement health over time
                SpawnParticles(hit.point);
            }
        }
    }
    public void SpawnTree(Vector3 position)
    {
        fallingTree = Instantiate(treePrefab, position, Quaternion.identity);
    }

    public void SpawnLogs()
    {
        StartCoroutine(SpawnLogsDelayed(3f));
    }

    private IEnumerator SpawnLogsDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(logPrefab, fallingTree.transform.position + new Vector3(0,1,0), Quaternion.identity);
        Instantiate(logPrefab, fallingTree.transform.position + new Vector3(1,1,1), Quaternion.identity);
        Instantiate(logPrefab, fallingTree.transform.position + new Vector3(2,1,2), Quaternion.identity);
        Destroy(fallingTree);
    }

    public void SpawnParticles(Vector3 position)
    {
        if (breakParticlesPrefab != null)
        {
            particles = Instantiate(breakParticlesPrefab, position, Quaternion.identity);
            particles.Play();

            //Make sure the program doesn't get overloaded by the particles
            ParticleSystem.MainModule mainModule = particles.main;
            float particleLifetime = mainModule.startLifetime.constant; // Get the constant value from the MinMaxCurve
            Destroy(particles.gameObject, particleLifetime + 0.1f); // Destroy particles after their lifetime + a small bufferr
        }
        else
        {
            Debug.LogWarning("breakParticlesPrefab is not assigned.");
        }
    }
}
