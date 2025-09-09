using UnityEngine;
using Unity.XR.CoreUtils;

public class FireHazard : MonoBehaviour
{
    [Header("Health")]
    public float maxHeat = 100f;
    public float heat = 100f;
    public bool extinguished;

    [Header("Damage")]
    public float damagePerSec = 20f;
    public float dangerRadius = 1.6f;

    [Header("Refs")]
    public ParticleSystem fireVFX;
    public Light fireLight;
    public AudioSource fireSFX;

    Transform player;
    PlayerHealth playerHealth;

    void Awake()
    {
        player = FindPlayerTransform();
        if (player) playerHealth = player.GetComponent<PlayerHealth>();
    }

    Transform FindPlayerTransform()
    {
        // Preferred: XR Origin
        var xr = FindObjectOfType<XROrigin>();
        if (xr) return xr.transform;

        // Fallback: use the main camera or its parent rig
        var cam = Camera.main ? Camera.main.transform : null;
        if (cam)
        {
            var xrFromCam = cam.GetComponentInParent<XROrigin>();
            return xrFromCam ? xrFromCam.transform : cam;
        }
        return null;
    }

    void Update()
    {
         if (extinguished || !player || !playerHealth) return;

        float d = Vector3.Distance(player.position, transform.position);
        if (d < dangerRadius)
            playerHealth.TakeDamage(damagePerSec * Time.deltaTime);
    }

    public void ApplyExtinguish(float amount)
    {
        if (extinguished) return;
        heat = Mathf.Max(0, heat - amount);
        if (heat <= 0) Extinguish();
    }

    void Extinguish()
    {
        extinguished = true;
        if (fireVFX && fireVFX.isPlaying) fireVFX.Stop();
        if (fireLight) fireLight.enabled = false;
        if (fireSFX && fireSFX.isPlaying) fireSFX.Stop();
        // Optionally: spawn smoke/steam VFX here
    }
}