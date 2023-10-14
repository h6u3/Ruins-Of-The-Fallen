using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FloraManager : MonoBehaviour {

    [SerializeField] private LayerMask mineableObject;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    public bool cooldown_running = false;
    public bool showUI = false;

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
                //Drop Items   
                Debug.Log("Harvested");
                cooldown_running = true;
                }
                else {
                    Debug.Log("No crops to harvest");
                }
            }
        }
    }

}
