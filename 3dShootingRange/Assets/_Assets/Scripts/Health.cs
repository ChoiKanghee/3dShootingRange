using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealthPoint;
    private int healthPoint;
    private bool isdead => healthPoint <= 0;
    private void Start()
    {
        healthPoint = maxHealthPoint;
    }
    public void takeDamage(int damage)
    {
        if (isdead) return;
        healthPoint -= damage;
        if (isdead)
        {
            Destroy(gameObject);
        }
    }
}
