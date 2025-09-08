using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMuzzle : MonoBehaviour
{
    public Transform muzzleImage;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        HideMuzzle();
    }
    public void ShowMuzzle()
    {
        muzzleImage.gameObject.SetActive(true);
        Debug.Log("tialua");
        float angle = Random.Range(0, 360f);
        muzzleImage.localEulerAngles = new Vector3(0, 0, angle);
        CancelInvoke();
        Invoke(nameof(HideMuzzle), duration);
    }
    public void HideMuzzle()
    {
        muzzleImage.gameObject.SetActive(false);
    }
    // Update is called once per frame
}
