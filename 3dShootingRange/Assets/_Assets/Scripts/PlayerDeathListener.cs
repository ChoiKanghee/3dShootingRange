using UnityEngine;

public class PlayerDeathListener : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public ZombieController[] zombies;

    void Start()
    {
        if (playerHealth == null && GameObject.FindWithTag("Player"))
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        if (playerHealth != null)
            playerHealth.OnPlayerDied += OnPlayerDied;
    }

    void OnPlayerDied()
    {
        Debug.Log("PlayerDeathListener detected player death");
        // Hành vi khi player chết: ngừng tất cả zombies tấn công
        foreach (var z in zombies)
        {
            if (z != null) z.SetState(ZombieController.State.Idle);
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnPlayerDied -= OnPlayerDied;
    }
}
