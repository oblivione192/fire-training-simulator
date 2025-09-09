using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class HandIKOnGrab : MonoBehaviour
{
    public TwoBoneIKConstraint rightArmIK;   // drag from Ren_BasicSetup/Rig/RightArm_Rig
    public Transform idleTarget;             // PlayerRig/Right Controller/RightControllerTarget
    public Transform gripTarget;             // Fire Extinguisher/RightHandAttach
    public float blendSpeed = 8f;

    XRGrabInteractable grab;
    void Awake(){
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(_ => StartGrip());
        grab.selectExited.AddListener(_ => EndGrip());
    }
    void StartGrip(){
        StopAllCoroutines();
        rightArmIK.data.target = gripTarget;
        StartCoroutine(Blend(1f));
    }
    void EndGrip(){
        StopAllCoroutines();
        rightArmIK.data.target = idleTarget;
        StartCoroutine(Blend(1f));
    }
    IEnumerator Blend(float t){
        while (Mathf.Abs(rightArmIK.weight - t) > 0.01f){
            rightArmIK.weight = Mathf.MoveTowards(rightArmIK.weight, t, Time.deltaTime * blendSpeed);
            yield return null;
        }
        rightArmIK.weight = t;
    }
}