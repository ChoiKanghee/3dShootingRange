using UnityEngine;

// Một lớp nhỏ để animation events gọi tới controller/attack module
public class ZombieAnimatorBridge : MonoBehaviour
{
    private ZombieController controller;
    private ZombieAttack attack;

    void Awake()
    {
        controller = GetComponent<ZombieController>();
        attack = GetComponent<ZombieAttack>();
    }

    // Gọi khi animation "attack" tới frame hit (animation event)
    // trong animation event đặt tên method OnAttackHit
    public void OnAttackHit()
    {
        if (controller != null)
            controller.OnAttackEvent_DoDamage(); // route
    }

    // Gọi ở bắt đầu/ket thúc animation attack
    public void OnAttackStart()
    {
        attack?.OnAttackStart();
    }
    public void OnAttackEnd()
    {
        attack?.OnAttackEnd();
    }
}
