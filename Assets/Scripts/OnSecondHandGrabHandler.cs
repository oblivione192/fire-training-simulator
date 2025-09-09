using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnSecondHandGrabHandler : MonoBehaviour
{
    private bool isSecondHandGrabbing = false;

    [Header("Reference Positions")]
    public Transform originalPosition; // Where object resets to
    public Transform secondHandReference; // The grab point for second hand

    [Header("Manual Offsets (set in Inspector)")]
    public Vector3 positionOffset; // X, Y, Z offset
    public Vector3 rotationOffsetEuler; // Rotation offset in Euler angles

    private XRGrabInteractable grabInteractable;
    private Quaternion rotationOffset;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("XRGrabInteractable component not found on " + gameObject.name);
        }
        else
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }

        // Convert inspector rotationOffsetEuler into Quaternion once
        rotationOffset = Quaternion.Euler(rotationOffsetEuler);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (grabInteractable.interactorsSelecting.Count < 2)
        {
            Debug.Log("Second Hand Released the Object");
            grabInteractable.transform.SetPositionAndRotation(originalPosition.position, originalPosition.rotation);
            isSecondHandGrabbing = false;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (grabInteractable.interactorsSelecting.Count >= 2)
        {
            Debug.Log("Second Hand Grabbed the Object");
            isSecondHandGrabbing = true;

            // Apply offsets
            grabInteractable.transform.position = originalPosition.TransformPoint(positionOffset);
            grabInteractable.transform.rotation = originalPosition.rotation * rotationOffset;

            Debug.Log("Second Hand Position & Rotation Set (with offset)");
        }
    }
}
