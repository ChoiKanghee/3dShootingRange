using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class GunAmmo : MonoBehaviour
{
    public int magSize;
    public float reFillTime;
    public Shooting shooting;
    public Animator anim; // không có
    public AudioSource reloadSound;  // có
    public UnityEvent loadedAmmoChanged;

    private bool isReloading;
    private int _loadedAmmo;
    //???
    public int LoadedAmmo
    {
        get => _loadedAmmo;
        set
        {
            _loadedAmmo = value;
            loadedAmmoChanged.Invoke();
            if (_loadedAmmo <= 0)
            {
                lockShooting();
            }
        }
    }
    //???
    private void Start()
    {
        refillAmmo();
        unlockShooting();
        Debug.Log(LoadedAmmo);
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        reloadSound.Play();
        lockShooting();
        yield return new WaitForSeconds(reFillTime);
        isReloading = false;
        refillAmmo();
        unlockShooting();
        Debug.Log("LoadedAmmo");
    }
    public void singleFireAmmoCounter() => LoadedAmmo--;
    public void lockShooting()
    {
        shooting.enabled = false;
    }
    private void unlockShooting()
    {
        shooting.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && LoadedAmmo != magSize)
        {
            StartCoroutine(Reload());
        }
    }
    //private void Reload()
    //{
    //    if (LoadedAmmo == magSize) return;

    //    lockShooting();
    //    AddAmmo();
    //    reloadSound.Play();
    //}
    public void AddAmmo() => refillAmmo();
    private void refillAmmo()
    {
        LoadedAmmo = magSize;
    }
    public void onGunSelected() => UpDateShootingLock();
    private void UpDateShootingLock()
    {
        shooting.enabled=_loadedAmmo > 0;
        isReloading = false;
    }
}
