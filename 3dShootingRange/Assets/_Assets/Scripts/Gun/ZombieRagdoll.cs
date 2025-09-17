using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class ZombieRagdoll : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Ragdoll")]
    public Transform ragdollRoot;
    public bool startRagdollDisabled = true;
    public float ragdollDisappearDelay = 8f;

    [Header("References")]
    public Animator animator;
    public Collider mainCollider;
    public Rigidbody mainRigidbody;

    private List<Rigidbody> ragdollRigidbodies;
    private List<Collider> ragdollColliders;
    private bool isRagdoll = false;

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
        foreach (var rb in ragdollRoot.GetComponentsInChildren<Rigidbody>())
            if (rb != mainRigidbody) ragdollRigidbodies.Add(rb);
        foreach (var col in ragdollRoot.GetComponentsInChildren<Collider>())
            if (col != mainCollider) ragdollColliders.Add(col);
    }

    public void TakeDamage(float dmg, Vector3 hitPoint, Vector3 hitNormal, HitSurface surface)
    {
        if (isRagdoll) return;
        currentHealth -= dmg;
        HitEffectManager.Instance.SpawnHitEffect(hitPoint, hitNormal, surface);
        if (currentHealth <= 0f) Die(hitPoint, hitNormal);
        else if (animator) animator.SetTrigger("Hit");
    }

    public void Die(Vector3 hitPoint, Vector3 hitNormal, Vector3 force = default)
    {
        if (isRagdoll) return;
        isRagdoll = true;
        if (animator) animator.enabled = false;
        if (mainCollider) mainCollider.enabled = false;
        if (mainRigidbody) mainRigidbody.isKinematic = true;
        SetRagdollActive(true);

        if (force != default)
            foreach (var rb in ragdollRigidbodies)
                rb.AddForceAtPosition(force, hitPoint, ForceMode.Impulse);

        StartCoroutine(RemoveAfterDelay(ragdollDisappearDelay));
    }

    void SetRagdollActive(bool on)
    {
        foreach (var rb in ragdollRigidbodies) rb.isKinematic = !on;
        foreach (var col in ragdollColliders) col.enabled = on;
    }

    IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void ForceDisableRagdoll()
    {
        StopAllCoroutines();
        isRagdoll = false;
        if (animator) animator.enabled = true;
        if (mainCollider) mainCollider.enabled = true;
        if (mainRigidbody) mainRigidbody.isKinematic = false;
        SetRagdollActive(false);
        currentHealth = maxHealth;
    }
}
