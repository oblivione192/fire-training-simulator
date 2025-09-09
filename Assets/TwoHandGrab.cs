using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrab : XRGrabInteractable
{
    [Header("Attach Points")]
    public Transform primaryAttachTransform;
    public Transform secondaryAttachTransform;

    private XRBaseInteractor primaryInteractor;
    private XRBaseInteractor secondaryInteractor;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (primaryInteractor == null) // First hand = main grip
        {
            primaryInteractor = args.interactorObject as XRBaseInteractor;
            attachTransform = primaryAttachTransform; // snap to grip
        }
        else if (secondaryInteractor == null) // Second hand = foregrip
        {
            secondaryInteractor = args.interactorObject as XRBaseInteractor;

            // Force the off-hand to snap to secondary attach
            secondaryInteractor.transform.position = secondaryAttachTransform.position;
            secondaryInteractor.transform.rotation = secondaryAttachTransform.rotation;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactorObject == primaryInteractor)
        {
            primaryInteractor = null;

            // If the main hand lets go, release off-hand too
            if (secondaryInteractor != null)
            {
                interactionManager.SelectExit(secondaryInteractor, this);
                secondaryInteractor = null;
            }
        }
        else if (args.interactorObject == secondaryInteractor)
        {
            secondaryInteractor = null;
        }
    }
}
