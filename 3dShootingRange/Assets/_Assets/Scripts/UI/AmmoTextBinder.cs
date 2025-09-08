using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoTextBinder : MonoBehaviour
{
    public TMP_Text loadedAmmoText;
    public GunAmmo gunAmmo;
    // Start is called before the first frame update
    void Update()
    {
        gunAmmo.loadedAmmoChanged.AddListener(UpdateGunAmmo);
        UpdateGunAmmo();
    }

    // Update is called once per frame
    public void UpdateGunAmmo()
    {
        loadedAmmoText.text = gunAmmo.LoadedAmmo.ToString();
    }
}
