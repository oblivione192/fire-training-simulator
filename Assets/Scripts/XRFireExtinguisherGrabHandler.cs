using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class XRFireExtinguisherGrabHandler : MonoBehaviour
{
    [Header("XR Grab Interactors")]
    public XRGrabInteractable extinguisherInteractable;
    public XRBaseInteractor primaryHandInteractor;
    public XRBaseInteractor secondaryHandInteractor;

    [Header("Grip Points")]
    public Transform primaryGripPoint;
    public Transform secondaryGripPoint;

    [Header("Sleeping Pose")]
    public Transform sleepingPoseTransform;
    public float transitionSpeed = 5f;

    private bool isPrimaryGrabbing = false;
    private bool isSecondaryGrabbing = false;
    private bool isInSleepingPose = false;
    private bool isTransitioning = false;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool originalPoseStored = false;

    void OnEnable()
    {
        extinguisherInteractable.selectEntered.AddListener(OnGrab);
        extinguisherInteractable.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        extinguisherInteractable.selectEntered.RemoveListener(OnGrab);
        extinguisherInteractable.selectExited.RemoveListener(OnRelease);
    }

    void Update()
    {
        if (isPrimaryGrabbing && isSecondaryGrabbing && !isInSleepingPose && !isTransitioning)
        {
            Debug.Log("Both hands grabbing: transitioning to sleeping pose.");
            StartCoroutine(MoveToPose(sleepingPoseTransform.position, sleepingPoseTransform.rotation, true));
        }
        else if (isPrimaryGrabbing && !isSecondaryGrabbing && isInSleepingPose && !isTransitioning)
        {
            Debug.Log("Secondary hand released: returning to original pose.");
            StartCoroutine(MoveToPose(originalPosition, originalRotation, false));
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (args.interactorObject == primaryHandInteractor)
        {
            isPrimaryGrabbing = true;
            Debug.Log("Primary hand grabbed extinguisher.");

            if (!originalPoseStored)
            {
                originalPosition = extinguisherInteractable.transform.position;
                originalRotation = extinguisherInteractable.transform.rotation;
                originalPoseStored = true;
                Debug.Log("Stored original pose.");
            }

            primaryHandInteractor.attachTransform = primaryGripPoint;
            Debug.Log("Primary hand attachTransform set.");
        }

        if (args.interactorObject == secondaryHandInteractor)
        {
            isSecondaryGrabbing = true;
            Debug.Log("Secondary hand grabbed extinguisher.");

            secondaryHandInteractor.attachTransform = secondaryGripPoint;
            Debug.Log("Secondary hand attachTransform set.");
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (args.interactorObject == primaryHandInteractor)
        {
            isPrimaryGrabbing = false;
            Debug.Log("Primary hand released extinguisher.");
        }

        if (args.interactorObject == secondaryHandInteractor)
        {
            isSecondaryGrabbing = false;
            Debug.Log("Secondary hand released extinguisher.");
        }
    }

    IEnumerator MoveToPose(Vector3 targetPosition, Quaternion targetRotation, bool toSleepingPose)
    {
        isTransitioning = true;

        float elapsed = 0f;
        Vector3 startPos = extinguisherInteractable.transform.position;
        Quaternion startRot = extinguisherInteractable.transform.rotation;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * transitionSpeed;

            extinguisherInteractable.transform.position = Vector3.Lerp(startPos, targetPosition, elapsed);
            extinguisherInteractable.transform.rotation = Quaternion.Slerp(startRot, targetRotation, elapsed);

            yield return null;
        }

        extinguisherInteractable.transform.position = targetPosition;
        extinguisherInteractable.transform.rotation = targetRotation;

        isInSleepingPose = toSleepingPose;
        isTransitioning = false;

        Debug.Log(toSleepingPose ? "Moved to sleeping pose." : "Returned to original pose.");
    }
}
