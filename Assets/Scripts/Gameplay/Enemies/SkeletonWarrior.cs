using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : Enemy {
    [Header("Skeleton Warrior")]
    [SerializeField] private Animator animator;
    [SerializeField] private AIController aiMovement;
    [SerializeField] private MeshCollider sword;
    [SerializeField] private SkinnedMeshRenderer eyes;

    protected override void Awake() {
        base.Awake();
        aiMovement.OnAttack += OnAttack;
        aiMovement.Init(this);
    }

    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
    }
    private void FixedUpdate() {
        float moveSpeed = 0;
        if (aiMovement.Velocity.magnitude > 0)
            moveSpeed = enemyAttributes.sprintSpeed / aiMovement.Velocity.magnitude;

        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    public void OnActiveSword() {
        sword.enabled = true;
    }
    public void OnDesativeSword() {
        sword.enabled = true;
        animator.SetInteger(Constants.Anim.ATTACK, -1);
    }
    private void OnAttack() {
        float dirX = (aiMovement.Target.position - transform.position).normalized.x;
        int attack = dirX > .2f ? 1 : dirX < -.2f ? -1 : 0; // 1 or -1 or 0
        animator.SetInteger(Constants.Anim.ATTACK, attack) ;
    }
    protected override void EnemyDeath() {
        aiMovement.StopAllCoroutines();
        sword.enabled = false;
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }
}
