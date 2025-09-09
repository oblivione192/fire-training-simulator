
using System;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;  

public class ReticleSpawner : MonoBehaviour
{
    private XRGrabInteractable theFireExtinguisher;
    private GameObject theReticle; 
    
    void Start()
    { 
        //theReticle = GameObject.Find("VR_Recticle_Square");  
        //if (theReticle != null)
        //    theReticle.SetActive(false); // Hide reticle initially

        //theFireExtinguisher = GetComponent<XRGrabInteractable>();
        //if (theFireExtinguisher == null)
        //{
        //    Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
        //}
        //else
        //{
        //    theFireExtinguisher.selectEntered.AddListener(OnGrab);
        //    theFireExtinguisher.selectExited.AddListener(OnRelease);
        //}
    }

    private void OnGrab(SelectEnterEventArgs args)
    {  
        //Debug.Log("Fire Extinguisher Grabbed");
        //if (theReticle != null)
        //    theReticle.SetActive(true); // Show reticle when grabbed
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        //Debug.Log("Fire Extinguisher Released");
        //if (theReticle != null)
        //    theReticle.SetActive(false); // Hide reticle when released
    }
}
