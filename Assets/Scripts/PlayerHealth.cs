using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float current = 100f;
    public GameObject dangerUI; // assign the DangerText (or a panel)
    float dangerHideTimer = 0f;

    public void TakeDamage(float amount)
    {
        current = Mathf.Max(0, current - amount);
        if (dangerUI) { dangerUI.SetActive(true); dangerHideTimer = 0.5f; }
        // TODO: handle death if needed
    }

    void Update()
    {
        if (dangerUI)
        {
            dangerHideTimer -= Time.deltaTime;
            if (dangerHideTimer <= 0f) dangerUI.SetActive(false);
        }
    }
}