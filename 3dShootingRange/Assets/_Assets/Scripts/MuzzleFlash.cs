using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public Transform muzzleEffect;
    public float duration;

    private void Start()
    {
        HideMuzzle();
    }

    public void ShowMuzzle()
    {
        muzzleEffect.gameObject.SetActive(true);
        float angle = Random.Range(0f, 360f);
        muzzleEffect.localEulerAngles = new Vector3(0, 0, angle);
        CancelInvoke();
        Invoke(nameof(HideMuzzle), duration);
    }

    private void HideMuzzle()
    {
        muzzleEffect.gameObject.SetActive(false);
    }
}
