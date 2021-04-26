using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SkeletonMage : Enemy {
    [Header("Skeleton Mage")]
    [SerializeField] private AIController aiController;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer eyes;
    [SerializeField] private Transform firePoint;

    [Header(" - Prefab")]
    [SerializeField] private PooledObject fireball;

    private Transform target;
    private Vector3 targetDirection;
    protected override void Awake() {
        base.Awake();
        aiController.Init(this);
        aiController.OnUpdateTarget += OnUpdateTarget;    // Estado de anim = armed
        aiController.OnStartAttack += OnAttack;           // Estado de anim = attack
        OnTakeDamage += aiController.OnTakeDamage;        // Evento ao receber dano (Detectar inimigo)
    }
    protected override void OnEnablePooledObject() {
        base.OnEnablePooledObject();
        eyes.material.color = Color.white;
    }
    private void FixedUpdate() {
        float moveSpeed = enemyAttributes.sprintSpeed / aiController.Velocity.magnitude;
        animator.SetFloat(Constants.Anim.MOVE_SPEED, moveSpeed);
    }
    private void OnUpdateTarget(Transform newTarget) {
        target = newTarget;
        animator.SetBool(Constants.Anim.IS_ARMED, newTarget);
    }

    private void OnAttack() {
        animator.SetTrigger(Constants.Anim.ATTACK);
        targetDirection = target.position + Vector3.up * 1.2f;
    }
    public void OnSpawnFireball() {
        firePoint.LookAt(targetDirection);
        fireball.SpawnObject(firePoint.position, firePoint.rotation);
    }
    protected override void EnemyDeath() {
        //aiMovement.StopAllCoroutines();
        aiController.Die();
        animator.SetTrigger(Constants.Anim.DEATH);
        eyes.material.color = Color.clear;
    }
}
