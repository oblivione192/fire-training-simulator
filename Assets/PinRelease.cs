using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PinSocketRelease : MonoBehaviour
{
    private XRGrabInteractable grab;
    public XRSocketInteractor socket; // drag extinguisher socket here

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // If the socket currently holds the pin, force it to release
        if (socket != null && socket.hasSelection && socket.firstInteractableSelected == grab)
        {
            socket.interactionManager.SelectExit(socket, grab);
        }
    }
}
