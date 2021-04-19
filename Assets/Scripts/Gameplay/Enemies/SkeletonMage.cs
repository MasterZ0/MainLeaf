using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]
    [SerializeField] private AIController aiController;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer eyes;
    [SerializeField] private Transform[] firePoint;

    [Header(" - Prefab")]
    [SerializeField] private PooledObject fireball;

    private int attack;
    private Vector3 targetPosition;

    protected override void Awake() {
        base.Awake();
        aiController.OnAttack += OnAttack;
        aiController.Init(this);
    }
    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
    }
    private void FixedUpdate() {
        float moveSpeed = enemyAttributes.sprintSpeed / aiController.Velocity.magnitude;
        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    private void OnAttack() {
        attack = UnityEngine.Random.Range(0, 2);
        animator.SetInteger(Constants.Anim.ATTACK, attack);
        firePoint[attack].LookAt(aiController.Target.position);
    }
    public void OnSpawnFireball() {
        animator.SetInteger(Constants.Anim.ATTACK, -1);        
        fireball.SpawnObject(firePoint[attack].position, firePoint[attack].rotation);
    }
    protected override void EnemyDeath() {
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }
}
