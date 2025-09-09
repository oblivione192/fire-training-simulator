using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireExtinguisherController : MonoBehaviour
{
    [Header("Fire Extinguisher Components")]
    public XRGrabInteractable grabInteractable;
    public Rigidbody rb;
    public Transform nozzle; // Assign the front part of fire extinguisher
    
    [Header("Hand Animation")]
    public Animator characterAnimator;
    public Transform rightHandBone;
    
    [Header("Grab Settings")]
    public bool isGrabbed = false;
    public string grabAnimationTrigger = "GrabExtinguisher";
    
    void Start()
    {
        // Get components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        
        // Setup grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }
    
    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        Debug.Log("Fire Extinguisher Grabbed!");
        
        // Trigger hand animation if available
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("IsHolding", true);
            characterAnimator.SetTrigger(grabAnimationTrigger);
        }
        
        // Optional: Add haptic feedback
        if (args.interactorObject is XRDirectInteractor interactor)
        {
            if (interactor.xrController != null)
            {
                interactor.xrController.SendHapticImpulse(0.5f, 0.2f);
            }
        }
    }
    
    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        Debug.Log("Fire Extinguisher Released!");
        
        // Reset hand animation
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("IsHolding", false);
        }
    }
    
    void Update()
    {
        if (isGrabbed)
        {
            // Here you can add additional logic while holding
            // For example: Show UI instructions, enable spray mode, etc.
        }
    }
}