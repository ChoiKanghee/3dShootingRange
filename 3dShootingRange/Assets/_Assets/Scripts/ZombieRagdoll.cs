using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieRagdoll : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public bool startRagdollDisabled = true;

    [Header("Ragdoll")]
    public Transform ragdollRoot; // root Transform that contains ragdoll bones with Rigidbodies
    public float ragdollDisappearDelay = 8f; // seconds before removing ragdoll
    public bool useUnscaledTimeForRemoval = false;

    [Header("References")]
    public Animator animator;
    public Collider mainCollider; // collider used while animated
    public Rigidbody mainRigidbody; // main rigidbody (if any)
    public HitEffectManager hitEffectManager; // optional

    float currentHealth;
    List<Rigidbody> ragdollRigidbodies;
    List<Collider> ragdollColliders;
    bool isRagdoll = false;

    void Awake()
    {
        currentHealth = maxHealth;
        if (animator == null) animator = GetComponent<Animator>();
        CacheRagdollParts();
        if (startRagdollDisabled) SetRagdollActive(false);
    }

    void CacheRagdollParts()
    {
        ragdollRigidbodies = new List<Rigidbody>();
        ragdollColliders = new List<Collider>();
        if (ragdollRoot == null) ragdollRoot = transform; // fallback

        foreach (var rb in ragdollRoot.GetComponentsInChildren<Rigidbody>())
        {
            if (rb == mainRigidbody) continue;
            ragdollRigidbodies.Add(rb);
        }

        foreach (var col in ragdollRoot.GetComponentsInChildren<Collider>())
        {
            if (col == mainCollider) continue;
            ragdollColliders.Add(col);
        }
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitNormal, HitSurface hitSurface = HitSurface.Default)
    {
        if (isRagdoll)
        {
            // When already ragdolled, we can still optionally apply force
            return;
        }

        currentHealth -= amount;
        // spawn hit effect
        if (hitEffectManager != null)
            hitEffectManager.SpawnHitEffect(hitPoint, hitNormal, hitSurface);

        if (currentHealth <= 0f)
        {
            Die(hitPoint, hitNormal);
        }
        else
        {
            // play hit animation / flinch if you want
            if (animator != null) animator.SetTrigger("Hit");
        }
    }

    public void Die(Vector3 hitPoint, Vector3 hitNormal, Vector3 force = default)
    {
        if (isRagdoll) return;
        isRagdoll = true;

        // disable animator & main collider/rigidbody
        if (animator != null) animator.enabled = false;
        if (mainCollider != null) mainCollider.enabled = false;
        if (mainRigidbody != null) mainRigidbody.isKinematic = true;

        SetRagdollActive(true);

        // apply explosion/impact force to ragdoll parts near hitPoint
        if (force != default)
        {
            foreach (var rb in ragdollRigidbodies)
            {
                rb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);
            }
        }

        // schedule ragdoll removal
        StartCoroutine(RemoveRagdollAfterDelay(ragdollDisappearDelay));
    }

    void SetRagdollActive(bool on)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = !on;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = on;
        }
    }

    IEnumerator RemoveRagdollAfterDelay(float delay)
    {
        float start = Time.time;
        if (useUnscaledTimeForRemoval) start = Time.unscaledTime;

        float target = start + delay;
        while ((useUnscaledTimeForRemoval ? Time.unscaledTime : Time.time) < target)
            yield return null;

        // optional: play vanish animation/effect then destroy
        Destroy(gameObject);
    }

    // Public helper to cleanly enable/disable ragdoll externally (for testing or editor tools)
    public void ForceEnableRagdoll()
    {
        Die(transform.position, Vector3.up);
    }

    public void ForceDisableRagdoll()
    {
        // Attempt to restore to animated state (useful with RagdollRemoverTool)
        StopAllCoroutines();
        isRagdoll = false;
        if (animator != null) animator.enabled = true;
        if (mainCollider != null) mainCollider.enabled = true;
        if (mainRigidbody != null) mainRigidbody.isKinematic = false;
        SetRagdollActive(false);
    }
}