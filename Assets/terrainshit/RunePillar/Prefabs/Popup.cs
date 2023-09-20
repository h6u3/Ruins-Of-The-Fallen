using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject RuinPillar;
    [SerializeField] private GameObject PopUp;
    [SerializeField] private Camera Camera;
    private GameObject newPop;

    private void Update()
    {
        if (Camera == null && newPop != null)
        {
            newPop.transform.LookAt(Camera.main.transform.position);
            newPop.transform.Rotate(new Vector3(0,180,0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the collider detectes a player, set true
        if (other.CompareTag("Player"))
        {
            if (newPop == null)
            {
                newPop = Instantiate(PopUp, (RuinPillar.transform.position + new Vector3(0,4,0)), Quaternion.identity);
                newPop.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the collider no longer detectes a player, set false
        if (other.CompareTag("Player"))
        {
            if (newPop != null)
            {
                Destroy(newPop);
            }
        }
    }


}
