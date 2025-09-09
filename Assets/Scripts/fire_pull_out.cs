
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_pull_out : MonoBehaviour
{
    private ParticleSystem fireEffect;  
    private AudioSource fireSound;
    public float shrinkSpeed = 0.05f; // Speed at which fire shrinks
    public AudioSource extinguishSound;
    public float extinguishDuration = 5f;
    private bool isExtinguished = false;

    private Vector3 originalScale;

    void Start()
    { 
        fireEffect = GetComponent<ParticleSystem>();  
        fireSound = GetComponent<AudioSource>();  
        if (fireEffect == null)
            Debug.LogError("No ParticleSystem found on fire object.");
        originalScale = transform.localScale;
    }

    // Called when a particle from any system hits this object's collider
    void OnParticleCollision(GameObject other)
    {  
        Debug.Log("Collided with: " + other.name);
        if (other.CompareTag("Extinguisher") && !isExtinguished)
        {
            Debug.Log("Fire hit by extinguisher!");
            StartCoroutine(ShrinkFire());
        }
    }

    public void OnDestroy()
    {  
        Debug.Log("Fire object destroyed.Stopping fire sound if playing.");
        if (fireSound != null)
            fireSound.Stop();
    }

    IEnumerator ShrinkFire()
    {
        isExtinguished = true;

        if (extinguishSound != null)
            extinguishSound.Play();

        if (fireEffect != null)
            fireEffect.Stop();

        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        

        while (transform.localScale.sqrMagnitude > 1e-6f)
        {
            transform.localScale = Vector3.MoveTowards(
                transform.localScale,
                Vector3.zero,
                shrinkSpeed * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}