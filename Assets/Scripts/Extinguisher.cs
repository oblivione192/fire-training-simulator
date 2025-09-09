using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Extinguisher : MonoBehaviour
{
    public Transform nozzleTip;
    public ParticleSystem sprayVFX;
    public AudioSource spraySFX;
    public LayerMask fireMask;
    public float sprayRange = 4f;
    public float extinguishPerSec = 15f;
    public bool pinPulled;

    XRGrabInteractable grab;
    bool spraying;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(OnActivate);
        grab.deactivated.AddListener(OnDeactivate);
    }

    public void PullPin() { pinPulled = true; }

    void OnActivate(ActivateEventArgs _)
    {
        if (!pinPulled) return;
        spraying = true;
        if (sprayVFX && !sprayVFX.isPlaying) sprayVFX.Play();
        if (spraySFX && !spraySFX.isPlaying) spraySFX.Play();
    }

    void OnDeactivate(DeactivateEventArgs _)
    {
        spraying = false;
        if (sprayVFX && sprayVFX.isPlaying) sprayVFX.Stop();
        if (spraySFX && spraySFX.isPlaying) spraySFX.Stop();
    }

    void Update()
    {
        if (!spraying || !nozzleTip) return;

        if (Physics.Raycast(nozzleTip.position, nozzleTip.forward, out RaycastHit hit, sprayRange, fireMask, QueryTriggerInteraction.Collide))
        {
            var hz = hit.collider.GetComponentInParent<FireHazard>();
            if (hz) hz.ApplyExtinguish(Time.deltaTime * extinguishPerSec);
        }
    }
}
