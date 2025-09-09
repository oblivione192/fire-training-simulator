using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePhysics : MonoBehaviour
{
    public GameObject particleSystem;
    private ParticleSystem ps;
    private Vector3 lastPosition;

    void Start()
    {
        if (particleSystem != null)
        {
            ps = particleSystem.GetComponent<ParticleSystem>();
            lastPosition = transform.position;
        }
    }

    void Update()
    {
        if (ps == null) return;

        // Calculate velocity of the emitter
        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;

        // Inherit velocity for trailing/curving effect
        var inheritVel = ps.inheritVelocity;
        inheritVel.enabled = true;
        inheritVel.mode = ParticleSystemInheritVelocityMode.Current;
        inheritVel.curveMultiplier = 2.0f; // Increase for more curve

        // Exaggerate the sweep with velocity over lifetime
        var velOverLifetime = ps.velocityOverLifetime;
        velOverLifetime.enabled = true;
        velOverLifetime.space = ParticleSystemSimulationSpace.World;
        velOverLifetime.x = new ParticleSystem.MinMaxCurve(velocity.x * 1.0f); // Increase multiplier
        velOverLifetime.y = new ParticleSystem.MinMaxCurve(velocity.y * 1.0f);
        velOverLifetime.z = new ParticleSystem.MinMaxCurve(velocity.z * 1.0f);

        lastPosition = transform.position;
    }
}