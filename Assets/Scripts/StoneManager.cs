using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    
    public LayerMask mineableObject;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private GameObject smallerCubePrefab;
    [SerializeField] private ParticleSystem breakParticlesPrefab;
    private ParticleSystem particles;
    private StoneHealth objectHealth;
    [SerializeField] private Hotbar hotbar;

    // Update is called once per frame
    void Update()
    {
        TryBreak();
    }

    public void TryBreak()
    {
        if (Input.GetMouseButton(0) && hotbar.selectedItem == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range, mineableObject))
            {
                objectHealth = hit.transform.GetComponent<StoneHealth>();
                objectHealth.health -= damage * Time.deltaTime; // Decrement health over time
                SpawnParticles(hit.point);
            }
        }
    }
    public void SpawnSmallerCube(Vector3 position)
    {
        position += new Vector3(0,1,0);
        Instantiate(smallerCubePrefab, position, Quaternion.identity);
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
