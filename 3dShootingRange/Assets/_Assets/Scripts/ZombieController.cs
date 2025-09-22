using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class ZombieController : MonoBehaviour
{
    public enum State { Idle, Moving, Attacking, Dead }
    public State currentState = State.Idle;

    [Header("Target")]
    public Transform target; // gán player transform

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float stoppingDistance = 1.5f; // gần player -> attack
    public float attackRange = 1.8f;
    public float attackCooldown = 2f;

    [Header("References")]
    public Animator animator;

    // internal
    private float lastAttackTime = -999f;
    private ZombieAttack attackModule;
    private Rigidbody rb;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        attackModule = GetComponent<ZombieAttack>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (target == null && GameObject.FindWithTag("Player"))
            target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (currentState == State.Dead) return;

        if (target == null)
        {
            SetState(State.Idle);
            return;
        }

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist > attackRange)
        {
            SetState(State.Moving);
            MoveTowardsTarget();
        }
        else
        {
            // in range to attack
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                SetState(State.Attacking);
                StartAttack();
            }
            else
            {
                // keep facing target but not attacking yet
                SetState(State.Idle);
                FaceTarget();
            }
        }
    }

    void SetState(State s)
    {
        if (currentState == s) return;
        currentState = s;

        // sync animator params
        animator.ResetTrigger("Die"); // safety
        animator.SetBool("isDead", s == State.Dead);
        animator.SetBool("isAttacking", s == State.Attacking);
        animator.SetBool("isMoving", s == State.Moving);
    }

    void MoveTowardsTarget()
    {
        Vector3 dir = (target.position - transform.position);
        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f) return;
        Vector3 move = dir.normalized * moveSpeed * Time.deltaTime;
        // simple move (CharacterController or Rigidbody approach possible)
        transform.position += move;
        FaceTarget();
    }

    void FaceTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f) return;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
    }

    void StartAttack()
    {
        lastAttackTime = Time.time;
        // set animator parameter; animation events will call attackModule.DoAttack()
        animator.SetBool("isAttacking", true);
    }

    public void OnAttackEvent_DoDamage()
    {
        // gọi bởi animation event / bridge
        if (attackModule != null) attackModule.PerformAttack();
    }

    // public method để set dead
    public void Die()
    {
        if (currentState == State.Dead) return;
        SetState(State.Dead);
        // remove any animation triggers so die is clean
        animator.ResetTrigger("Attack"); // nếu có trigger tên Attack
        animator.SetBool("isAttacking", false);
        animator.SetBool("isMoving", false);
        // play die trigger if needed
        animator.SetTrigger("Die");
        // disable colliders / nav / ai as necessary
        // optional: Destroy(gameObject, 5f);
    }
}
