using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float boostSpeed;
    private bool isBoosted;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            if (!isBoosted)
            {
                
                var bike = other.gameObject.GetComponent<BikeAcceleratorController>();
                bike.BoostSpeed(boostSpeed);
                bike.isBikeBoosted = true;
                bike.BikeCamera.GetComponent<CameraFollow>().isFirstPersonSelected = false;
                isBoosted = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        StartCoroutine(BikeBoostReset(other));
    }

    IEnumerator BikeBoostReset(Collider other)
    {
        var bike = other.gameObject.GetComponent<BikeAcceleratorController>();
        yield return new WaitForSeconds(2.0f);
        bike.isBikeBoosted = false;
        //bike.BikeCamera.GetComponent<CameraFollow>().isFirstPersonSelected = true;
        isBoosted = false;

    }
}
