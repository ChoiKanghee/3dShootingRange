using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!player) return;
        agent.SetDestination(player.position);
        if (animator) animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}