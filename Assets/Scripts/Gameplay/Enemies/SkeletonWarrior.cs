using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;
    [SerializeField] private AIMovement AIMovement;
    [SerializeField] private MeshCollider sword;
    [SerializeField] private SkinnedMeshRenderer eyes;
    protected override void AwakeEnemy() {
        base.AwakeEnemy();
        ResetEnemy();
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
        sword.enabled = true;
        return;
        animator.SetBool(Constants.Anim.ATTACK, true);
        AIMovement.CheckTargetDistance();
        sword.enabled = false;
    }
    protected override void EnemyDeath() {
        print("Is Dead");
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }

    private void FixedUpdate() {
        UpdateAnimations();
    }

    private void UpdateAnimations() {
        animator.SetFloat(Constants.Anim.VELOCITY_Y, AIMovement.Velocity.z);
    }
}
