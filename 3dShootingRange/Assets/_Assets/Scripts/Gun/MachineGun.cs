using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public float fireRate = 10f;
    public float damage = 15f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    public LayerMask hitMask;

    float nextFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (muzzleFlash) muzzleFlash.Play();
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            var surface = HitSurface.Default;
            var detector = hit.collider.GetComponent<HitSurfaceDetector>();
            if (detector) surface = detector.surface;

            HitEffectManager.Instance.SpawnHitEffect(hit.point, hit.normal, surface);

            var zombie = hit.collider.GetComponentInParent<ZombieRagdoll>();
            if (zombie) zombie.TakeDamage(damage, hit.point, hit.normal, surface);
        }
    }
}
