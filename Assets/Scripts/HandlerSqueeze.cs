using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerSqueeze : MonoBehaviour
{
    // Start is called before the first frame update 
    private bool isSqueezed = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        if (other.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRBaseController>() != null)
        {
            isSqueezed = true;
            Debug.Log("XR Controller touched the Handler!");
        }
        else
        {
            Debug.Log("Non-XR object touched the Handler!");
        }
   }
     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand left the button trigger!");
        }
    }
}
