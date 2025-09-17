using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Shooting
{
    private const int leftMouseButton = 0;
    public GameObject bulletPrefab;
    public Transform firingPos;
    public float bulletSpeed;
    public AudioSource shootingSound;
    public Animator anim;
    public GunAmmo gunAmmo;

    void Update()
    {
        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            Debug.Log("Bullet Fired");
            ShootBullet();
        }
	}

    private void ShootBullet()
    {
        Debug.Log("ShootBullet method called");
        anim.SetTrigger("Shoot");
    }

    public void PlayFireSound()
    {
        Debug.Log("PlayFireSound method called");
        shootingSound.Play();
    }

    public void AddProjectile()
    {
        Debug.Log("AddProjectile method called");
        GameObject bullet = Instantiate(bulletPrefab, firingPos.position, firingPos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firingPos.forward * bulletSpeed;
        gunAmmo.SingleFireAmmoCounter();
	}
}
