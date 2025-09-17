using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float fuseTime = 2f;
    public float radius = 6f;
    public float force = 700f;
    public float maxDamage = 200f;
    public LayerMask damageMask;
    public GameObject explosionVFX;

    void Start() => StartCoroutine(Fuse());

    IEnumerator Fuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    void Explode()
    {
        if (explosionVFX) Instantiate(explosionVFX, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, damageMask);
        foreach (var hit in hits)
        {
            var zombie = hit.GetComponentInParent<ZombieRagdoll>();
            if (zombie)
            {
                float dist = Vector3.Distance(transform.position, hit.ClosestPoint(transform.position));
                float t = Mathf.Clamp01(1 - dist / radius);
                float dmg = Mathf.Lerp(0, maxDamage, t);
                Vector3 dir = (hit.transform.position - transform.position).normalized;
                zombie.TakeDamage(dmg, hit.ClosestPoint(transform.position), dir, HitSurface.Flesh);
                zombie.Die(hit.ClosestPoint(transform.position), dir, dir * force * t);
            }
        }
        Destroy(gameObject);
    }
}
