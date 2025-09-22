using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public event Action OnPlayerDied; // event để người khác subscribe

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;
        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. HP = {currentHealth}");
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLAYER DIED"); // Yêu cầu: in debug log khi player died
        OnPlayerDied?.Invoke();
        // thêm hiệu ứng, disable controls, animator trigger etc tùy game
    }
}
