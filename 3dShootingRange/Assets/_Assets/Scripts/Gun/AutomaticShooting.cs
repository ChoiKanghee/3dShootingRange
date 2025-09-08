using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutomaticShooting : MonoBehaviour
{
    public Animator anim;
    public int rpm;
    public GameObject hitMarkerPrefab;
    public Camera aimingCamera;
    public LayerMask layerMask;
    public AudioSource shootSound;
    public UnityEvent onShoot;

    private float lastShoot;
    private float interval;

    private void Start() => interval = 60f / rpm;

    private void Update() { }

    private void UpdateFiring() { }

    private void Shoot()
    {
        anim.Play("Shoot", layer: -1, normalizedTime: 0);
        shootSound.Play();
        PerformRaycasting();
        onShoot.Invoke();
    }

    private void PerformRaycasting()
    {
        Ray aimingRay = new Ray(aimingCamera.transform.position, aimingCamera.transform.forward);
        if (Physics.Raycast(aimingRay, out RaycastHit hitInfo, 1000f, layerMask))
        {
            Quaternion effectRotation = Quaternion.LookRotation(hitInfo.normal);
            Instantiate(hitMarkerPrefab, hitInfo.point, effectRotation);
        }
    }
}
