using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ZombieAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 25;
    public float attackRadius = 1.2f;
    public Transform attackOrigin; // vị trí phát hiện hit (gán vào muzzle hoặc chest)
    public LayerMask damageLayers; // layer mask cho player

    [Header("Timing")]
    public float hitWindowStart = 0.15f; // nếu dùng internal timing
    public float hitWindowEnd = 0.35f;

    // optional visual debug
    private void OnDrawGizmosSelected()
    {
        if (attackOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
        }
    }

    // Called from animation event (when attack "actually" hits)
    public void PerformAttack()
    {
        Vector3 origin = attackOrigin != null ? attackOrigin.position : transform.position + transform.forward * 0.8f;
        Collider[] hits = Physics.OverlapSphere(origin, attackRadius, damageLayers);
        foreach (var col in hits)
        {
            PlayerHealth ph = col.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(damage);
            }
        }
        // Optional: play sound, spawn VFX, etc.
        Debug.Log("Zombie performed attack");
    }

    // These could be called by animation events if you want to toggle a hitbox on/off
    public void OnAttackStart()
    {
        Debug.Log("AttackStart event called");
        // e.g., enable hitbox collider
    }

    public void OnAttackEnd()
    {
        Debug.Log("AttackEnd event called");
        // disable hitbox collider
    }
}
