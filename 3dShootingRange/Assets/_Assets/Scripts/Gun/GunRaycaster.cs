using UnityEngine;

public class GunRaycaster : MonoBehaviour
{
    public Camera aimingCamera;
    public LayerMask layerMask;
    public int damage = 10;

    public void PerformRaycasting()
    {
        Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000f, layerMask))
        {
            ShowHitEffect(hitInfo);
            DeliverDamage(hitInfo);
        }
    }

    private void ShowHitEffect(RaycastHit hitInfo)
    {
        HitSurfaceDetector detector = hitInfo.collider.GetComponent<HitSurfaceDetector>();
        HitSurface surface = detector ? detector.surface : HitSurface.Default;

        GameObject effectPrefab = HitEffectManager.Instance.GetEffectPrefab(surface);
        if (effectPrefab != null)
        {
            Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            Instantiate(effectPrefab, hitInfo.point, effectRotation);
        }
    }

    private void DeliverDamage(RaycastHit hitInfo)
    {
        ZombieRagdoll zombie = hitInfo.collider.GetComponentInParent<ZombieRagdoll>();
        if (zombie != null)
        {
            HitSurfaceDetector detector = hitInfo.collider.GetComponent<HitSurfaceDetector>();
            HitSurface surface = detector ? detector.surface : HitSurface.Flesh;

            zombie.TakeDamage(damage, hitInfo.point, hitInfo.normal, surface);
        }
    }
}
