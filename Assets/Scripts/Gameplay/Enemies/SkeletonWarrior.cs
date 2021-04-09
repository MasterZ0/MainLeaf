using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;
    [SerializeField] private AIMovement AIMovement;

    private NavMeshAgent navMeshAgent;
    protected override void AwakeEnemy() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected override void ResetEnemy() {
        // Walk Mode
        enemyState = EnemyState.Patrolling;
        AIMovement.Patrol(FindedPlayer);
    }

    private void FindedPlayer() {
        // Run Mode
        enemyState = EnemyState.Chasing;
        AIMovement.ChaseTarget(Attack, () => AIMovement.Patrol(FindedPlayer));
    }
    private void Attack() {
        // Attack
        enemyState = EnemyState.Attacking;
        print("Atck");
        return;
        animator.SetBool(Constants.Anim.ATTACK, true);
        AIMovement.CheckTargetDistance();
    }
    protected override void EnemyDeath() {
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        UpdateAnimations();
    }

    private void UpdateAnimations() {
        Vector3 velocity = navMeshAgent.velocity;
        animator.SetFloat(Constants.Anim.VELOCITY_Y, velocity.y);
    }
}
