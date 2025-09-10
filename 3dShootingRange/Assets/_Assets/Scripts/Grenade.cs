using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float fuseTime = 2.0f;
    public float explosionRadius = 6f;
    public float explosionForce = 700f;
    public float maxDamage = 200f;
    public LayerMask damageLayerMask; // e.g. include "Zombie" layer
    public GameObject explosionVFX;
    public AudioClip explosionSFX;
    public bool destroyOnExplode = true;

    void Start()
    {
        StartCoroutine(Fuse());
    }

    IEnumerator Fuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    public void Explode()
    {
        // spawn VFX and SFX
        if (explosionVFX) Instantiate(explosionVFX, transform.position, Quaternion.identity);
        if (explosionSFX) AudioSource.PlayClipAtPoint(explosionSFX, transform.position);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, damageLayerMask);
        foreach (var col in hits)
        {
            // check if the hit object has a ZombieRagdoll
            ZombieRagdoll zr = col.GetComponentInParent<ZombieRagdoll>();
            if (zr != null)
            {
                // calculate damage based on distance
                Vector3 closestPoint = col.ClosestPoint(transform.position);
                float dist = Vector3.Distance(transform.position, closestPoint);
                float t = Mathf.Clamp01(1f - (dist / explosionRadius));
                float damage = Mathf.Lerp(0, maxDamage, t);

                // apply force to rigidbodies if ragdoll parts exist, else to main rigidbody
                Vector3 dir = (col.transform.position - transform.position).normalized;
                Vector3 force = dir * (explosionForce * t);
                zr.TakeDamage(damage, closestPoint, dir == Vector3.zero ? Vector3.up : dir);
                zr.Die(closestPoint, dir, force); // ensure ragdoll enable when killed
            }
            else
            {
                // apply physics to rigidbody if available (breakable crates, props)
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        if (destroyOnExplode) Destroy(gameObject);
    }
}