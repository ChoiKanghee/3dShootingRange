using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public float fireRate = 10f; // bullets per second
    public float damagePerShot = 15f;
    public float range = 100f;
    public Camera shootCamera;
    public ParticleSystem muzzleFlash;
    public LayerMask hitMask;
    public HitEffectManager hitEffectManager;

    float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + (1f / fireRate);
        }
    }

    void Fire()
    {
        if (muzzleFlash) muzzleFlash.Play();

        Ray ray;
        if (shootCamera != null)
            ray = new Ray(shootCamera.transform.position, shootCamera.transform.forward);
        else
            ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            // detect hit surface type (via component or tag)
            HitSurface surface = HitSurface.Default;
            var detector = hit.collider.GetComponent<HitSurfaceDetector>();
            if (detector != null) surface = detector.surface;

            // spawn hit VFX
            if (hitEffectManager != null)
                hitEffectManager.SpawnHitEffect(hit.point, hit.normal, surface);

            // if hit a zombie
            ZombieRagdoll zr = hit.collider.GetComponentInParent<ZombieRagdoll>();
            if (zr != null)
            {
                zr.TakeDamage(damagePerShot, hit.point, hit.normal, surface);
            }
            else
            {
                // apply physics impulse for props
                Rigidbody rb = hit.collider.attachedRigidbody;
                if (rb != null) rb.AddForceAtPosition(-hit.normal * 50f, hit.point, ForceMode.Impulse);
            }
        }
    }
}