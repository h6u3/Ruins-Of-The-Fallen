using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FloraManager : MonoBehaviour {

    [SerializeField] private LayerMask mineableObject;
    [SerializeField] private GameObject Item;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private  FloraItemManager objectHealth;
    public bool cooldown_running = false;
    public bool showUI = false;
    public bool harvested = false;

    // Update is called once per frame
    void Update()
    {
        checkLooking();
        TryHarvest();
    }
    
    public void checkLooking() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, mineableObject))
        {   
            showUI = true;
        }
        else {
            showUI = false;
        }
    }

    public void TryHarvest()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, range, mineableObject))
            {   
                if(!cooldown_running) {
                Debug.Log("Harvested");
                objectHealth = hit.transform.GetComponent<FloraItemManager>();
                objectHealth.health -= damage;
                cooldown_running = true;
                }
                else {
                    Debug.Log("No crops to harvest");
                }
            }
        }
    }

    public void SpawnFloraItems(Vector3 position)
    {
        position += new Vector3(0,1,0);
        Instantiate(Item, position, Quaternion.identity);
    }

    public void ResetPlant() {
        cooldown_running = false;
        harvested = false;
        objectHealth.health = 1;
    }
}
