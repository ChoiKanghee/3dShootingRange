using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRayCaster : MonoBehaviour
{
    //public GameObject hitMarkerPrefab;
    public Camera aimingCamera;
    public LayerMask layerMask;
    public int damage;

    public void PerformRayCasting()
    {
        Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000f, layerMask))
        {
            //Debug.Log("Hit: " + hitInfo.collider.name);
            //Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            //Instantiate(hitMarkerPrefab, hitInfo.point, effectRotation);
            ShowHitEffect(hitInfo);
            DealDamage(hitInfo);
        }
    }

    private void ShowHitEffect(RaycastHit hitInfo)
    {
        HitSurface hitSurface = hitInfo.collider.GetComponent<HitSurface>();
        if (hitSurface != null)
        {
            GameObject effectPrefab = HitEffectManager.Instance.GetEffectPrefab(hitSurface.surfaceType);
            if ( effectPrefab != null)
            {
                Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
                Instantiate(effectPrefab, hitInfo.point, effectRotation);
            }
        }
    }
    private void DealDamage(RaycastHit hitInfo)
    {
        Health health = hitInfo.collider.GetComponentInParent<Health>();
        if (health != null)
        {
            Debug.Log("Dealing " + damage + " damage to " + hitInfo.collider.name);
            health.TakeDamage(damage);
        }
    }
}
