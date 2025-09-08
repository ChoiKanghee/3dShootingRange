using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    public GameObject[] guns;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                SetActiveGun(i);
            }
        }
    }
    private void SetActiveGun(int index)
    {
        for(int i=0;i<guns.Length;i++)
        {
            bool isActive =(i==index);
            guns[i].SetActive(isActive);
            if (isActive)
            {
                guns[i].SendMessage("onGunSelected",SendMessageOptions.DontRequireReceiver);
            }
        }
        
    }
}
