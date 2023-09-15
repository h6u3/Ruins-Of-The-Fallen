using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem breakParticlesPrefab;
    [SerializeField] private LayerMask mineableObject;
    [SerializeField] private StoneHealth objectHealth;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    public GameObject smallerCubePrefab;
    private ParticleSystem particles;
    public int minedCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TryBreak();
    }

    public void LoadData(GameData data)
    {
        this.minedCount = data.blocksMined;
    }

    public void SaveData(ref GameData data)
    {
        data.blocksMined = this.minedCount;
    }

    public void TryBreak()
    {
        if (Input.GetMouseButton(0))
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
            Destroy(particles.gameObject, particleLifetime + 0.5f); // Destroy particles after their lifetime + a small bufferr
        }
        else
        {
            Debug.LogWarning("breakParticlesPrefab is not assigned.");
        }
    }
}
