using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AllowPinGrab : XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        // Don't block selection if parent is already grabbed
        base.OnSelectEntering(args);
    }
}