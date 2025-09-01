// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class GunAmmo : MonoBehaviour
// {
//     public int magSize;
//     public GrenadeLauncher gun;
//     public Animator anim;
//     public AudioSource[] reloadSounds;
//     public UnityEvent loadedAmmoChanged;

//     private int _loadedAmmo;
//     public int LoadedAmmo
//     {
//         get => _loadedAmmo;
//         set
//         {
//             _loadedAmmo = value;
//             loadedAmmoChanged.Invoke();

//             if (_loadedAmmo <= 0)
//             {
//                 Reload();
//             }
//             else
//             {
//                 UnlockShooting();
//             }
//         }
//     }

//     private void Start() => RefillAmmo();

//     private void SingleFireAmmoCounter() => LoadedAmmo--;

//     private void LockShooting() => gun.enabled = false;

//     private void UnlockShooting() => gun.enabled = true;

//     private void Update()
//     {
//     }

//     private void Reload()
//     {
//         // logic reload animation / sound
//     }

//     private void AddAmmo() => RefillAmmo();

//     private void RefillAmmo() => LoadedAmmo = magSize;

//     public void PlayReloadPart1Sound() => reloadSounds[0].Play();
//     public void PlayReloadPart2Sound() => reloadSounds[1].Play();
//     public void PlayReloadPart3Sound() => reloadSounds[2].Play();
//     public void PlayReloadPart4Sound() => reloadSounds[3].Play();
//     public void PlayReloadPart5Sound() => reloadSounds[4].Play();
// }
