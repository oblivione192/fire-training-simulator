using UnityEngine;

public class AvatarMotions : MonoBehaviour
{
    public CharacterController rigCC;   // PlayerRig CC
    public Animator animator;           // Ren Animator
    public Transform cameraT;           // Main Camera
    public float turnSpeed = 8f;

    void Update()
    {
        var v = rigCC ? new Vector3(rigCC.velocity.x,0,rigCC.velocity.z) : Vector3.zero;
        animator.SetFloat("Speed", v.magnitude);
        if (v.sqrMagnitude > 0.0025f)
        {
            var f = new Vector3(cameraT.forward.x,0,cameraT.forward.z).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(f), Time.deltaTime*turnSpeed);
        }
    }
}