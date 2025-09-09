using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorXRI : MonoBehaviour
{
    public Animator animator;
    public InputActionProperty grip;     // XRI RightHand Interaction/Select Value
    public InputActionProperty trigger;  // XRI RightHand Interaction/Activate Value

    void Update()
    {
        float g = grip.action?.ReadValue<float>() ?? 0f;
        float t = trigger.action?.ReadValue<float>() ?? 0f;
        animator.SetFloat("Grip", g);
        animator.SetFloat("Trigger", t);
    }
}
