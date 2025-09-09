using System.Collections;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float openAngle = 90f;   // degrees
    public float openSpeed = 2f;    // degrees per second

    private bool isOpen = false;
    private bool isRotating = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, openAngle, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        // Prefer tags in real projects: other.CompareTag("Player")
        if (other.name == "PlayerRig Variant" && !isOpen && !isRotating)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(openRotation, opening:true));
    }

    // Optional close method if you ever need it (not used here)
    private void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(closedRotation, opening:false));
    }

    private IEnumerator RotateDoor(Quaternion targetRotation, bool opening)
    {
        isRotating = true;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                openSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = opening;
        isRotating = false;

        // If you want to completely ignore future triggers, uncomment:
        // if (isOpen) GetComponent<Collider>().enabled = false;
    }
}