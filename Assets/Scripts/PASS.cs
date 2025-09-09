using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class PASS : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [Header("Fire extinguisher parts")]
    public GameObject pin;
    public GameObject fireHose;
    public GameObject handle;
    public ParticleSystem foamEffect;

    private bool pinPulled;
    private bool isFiring;
    private float fireHoldTime = 0f; // keep if you need a delay
    private float holdTimer;

    private AudioSource hissingAudio;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (!grabInteractable)
        {
            Debug.LogError("XRGrabInteractable missing on " + name);
            enabled = false; return;
        }

        // Fire-extinguisher grab/release
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);

        // Trigger press/release (Activate = trigger in XRI default actions)
        grabInteractable.activated.AddListener(OnActivated);
        grabInteractable.deactivated.AddListener(OnDeactivated);

        // Pin grab (register once)
        if (pin)
        {
            var pinGrab = pin.GetComponent<XRGrabInteractable>();
            if (pinGrab) pinGrab.selectEntered.AddListener(OnPinGrabbed);
        }
    }

    void Start()
    {
        var hissObj = GameObject.Find("Hissing Sound Effect");
        if (hissObj) hissingAudio = hissObj.GetComponent<AudioSource>();

        pinPulled = false;
        isFiring = false;
        holdTimer = 0f;
        if (foamEffect) foamEffect.Stop();
    }

    // --- Events -------------------------------------------------------------

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Fire extinguisher grabbed.");
        holdTimer = 0f;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        holdTimer = 0f;
        if (isFiring) StopFiring();
        if (handle) handle.transform.localRotation = Quaternion.identity;
    }

    private void OnActivated(ActivateEventArgs args)
    {
        if (!pinPulled)
        {
            Debug.Log("You cannot fire the extinguisher without pulling the pin!");
            return;
        }

        holdTimer += Time.deltaTime;
        if (handle) handle.transform.localRotation = Quaternion.Euler(0f, 0f, 24.345f);

        if (!isFiring && holdTimer >= fireHoldTime)
            StartFiring();
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        holdTimer = 0f;
        if (handle) handle.transform.localRotation = Quaternion.identity;
        if (isFiring) StopFiring();
    }

    private void OnPinGrabbed(SelectEnterEventArgs args)
    {
        pinPulled = true;
        Debug.Log("Pin pulled! Hold trigger to fire.");
    }

    // --- Helpers ------------------------------------------------------------

    private void StartFiring()
    {
        isFiring = true;
        if (hissingAudio) hissingAudio.Play();
        if (foamEffect) foamEffect.Play();
        Debug.Log("Extinguisher firing!");
    }

    private void StopFiring()
    {
        isFiring = false;
        if (hissingAudio) hissingAudio.Pause();
        if (foamEffect) foamEffect.Stop();
        Debug.Log("Extinguisher stopped.");
    }
}