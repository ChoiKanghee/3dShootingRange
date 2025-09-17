using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTextBinder : MonoBehaviour
{
    public TMP_Text loadedAmmoText;
    public GunAmmo gunAmmo;

    private void Start()
    {
        gunAmmo.loadAmmoChanged.AddListener(UpdateGunAmmo);
        UpdateGunAmmo();
    }

    public void UpdateGunAmmo()
    {
        loadedAmmoText.text = gunAmmo.LoadedAmmo.ToString();
    }
}
