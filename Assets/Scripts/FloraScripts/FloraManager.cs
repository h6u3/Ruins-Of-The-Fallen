using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FloraManager : MonoBehaviour {

    [SerializeField] private LayerMask mineableObject;
    
    [SerializeField] private float damage;
    [SerializeField] private float range;
    private  FloraItemManager objectHealth;
    public bool cooldown_running = false;
    public bool showUI = false;
    public bool harvested = false;

    // Update is called once per frame
    void Update()
    {
        checkLooking();
        tryHarvest();
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

    public void tryHarvest()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, range, mineableObject))
            {   
                if(!cooldown_running) {
                objectHealth = hit.transform.GetComponent<FloraItemManager>();
                objectHealth.health -= damage;
                cooldown_running = true;
                }
            }
        }
    }

    public void resetPlant() {
        cooldown_running = false;
        harvested = false;
        objectHealth.health = 1;
    }
}
